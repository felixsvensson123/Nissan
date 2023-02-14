using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity;
using N_Chat.Shared.dto;

//using AutoMapper;

namespace N_Chat.Server.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly DataContext context;
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;

        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, DataContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        
        [HttpGet("userchats/{id}")]
        public async Task<IEnumerable<ChatModel>> GetUserChats(string id)
        {
            List<ChatModel> dbChats = await context.Chats.Where(x => x.UserId == id)
                .Include(t => t.Messages)
                .Include(t => t.User).ToListAsync();
            return dbChats;
        }

        //Get User by ID
        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            UserModel currentuser = await userManager.FindByNameAsync(id); //gets current user using recieved ID
            if (currentuser != null)
                return Ok(currentuser);
            return BadRequest(currentuser);
        }
        
        [Authorize]
        [HttpGet("getcurrent")] //get current user from claim
        public async Task<ActionResult> GetCurrentUser()
        {
            var uid = User.FindFirst(ClaimTypes.Name)?.Value; //Finds user claim

            UserModel? user = await context.Users //finds user that matches claim
                .FirstOrDefaultAsync(u => u.UserName == uid);
            UserModel dtoUser = new UserModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName
            };
            return Ok(user); //returns found user
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager
                    .PasswordSignInAsync(user.Username, user.Password, true, false); // signs in user.
                if (result.Succeeded)
                {
                    UserModel currentUser =
                        await signInManager.UserManager.FindByNameAsync(user.Username); // gets current user by username
                    var claims = new List<Claim> //sets up userid claim
                    {
                        new(ClaimTypes.Name,
                            currentUser.UserName) // Sets claimtype to name, with value: user's username
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    
                    await HttpContext.SignInAsync( //sets claims principals and signs in use to HttpContext.
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return Ok(result);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt"); // Error Message if string is empty
            }

            return BadRequest(user);
        }

        [HttpPost("signup")]

        public async Task<IActionResult> SignupUser(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var checkUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == registerModel.Username); //Checks if user already exists in DB
                if (checkUser != null)
                {
                    return BadRequest(("Username taken") + checkUser);
                }
                var user = new UserModel() // sets usermodel props to registerModel props
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                
                var result = await userManager.CreateAsync(user, registerModel
                    .Password); // Creates user account sets after usermodel and registermodels password

                if (result.Succeeded) // Checks if createasync succeded
                {
                    await userManager.AddToRoleAsync(user, "Member"); // sets register'd users role to "Member"
                    return Ok(result);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            await signInManager.SignOutAsync();
            return Ok("Successful sign out!");
        }


        // Update email for user
        [HttpPut("edituser")]
        public async Task<IActionResult> UpdateUsersMail(UserModel updateModel)
        {
            if (ModelState.IsValid)
            {
                var checkUser =
                    await signInManager.UserManager.FindByIdAsync(updateModel.Id); // Gets current user using ID

                if (checkUser == null)
                {
                    return BadRequest();
                }

                checkUser.Email = updateModel.Email;
                context.Update(checkUser);
                await context.SaveChangesAsync();
                return Ok();
            }


            return BadRequest(updateModel);
        }

        [HttpPost("ListMessages")]
        public async Task<ActionResult<List<MessageModel>>> GetMessages(MessageModel messageModel)
        {
            var CurrentUser = await userManager.FindByIdAsync(messageModel.UserId);
            List<MessageModel> messages = new();
            using (var db = context){
                messages = await db.Messages.Where(m => m.UserId == messageModel.UserId).ToListAsync();
                
                foreach (var message in messages)
                {
                    CurrentUser.Messages.Add(message);
                }

                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("ChatList")]
        public async Task<ActionResult<List<ChatModel>>> AddChatsToUser(string userId)
        {
            var CurrentUser = await userManager.FindByIdAsync(userId);

            List<ChatModel> chats = new();

            await using (var db = context)
            {
                chats = await db.Chats.Where(c => c.UserId == userId).ToListAsync();

                foreach (var chat in chats)
                {
                    CurrentUser.Chats.Add(chat);
                }

                await db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPut("chatrequest/{chatId}")]
        public async Task<ActionResult> RequestChat(int chatId, string userName)
        {
            await using (var db = context)
            {
                var user = await userManager.FindByNameAsync(userName); //finds user with username recieved
                if (user != null)
                {
                    var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == chatId); //finds chat using id recieved
                    if (chat != null)
                    {
                        chat.Users.Add(user); //adds recieved user to chat
                        return Ok(chat);
                    }

                    return BadRequest(chat);
                }

                return BadRequest(user);
            }
        }
    }
}