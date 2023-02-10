using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity;
using MongoDB.Driver.Linq;
using N_Chat.Shared.dto;

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
                

        [HttpGet("userchats/{userName}")]
        public async Task<IEnumerable<ChatModel>> GetUserChats(string userName)
        {
            List<ChatModel> dbChats = await context.Chats
                .Include(u => u.Users.Where(x=> x.UserName == userName))
                .Where(u=>u.UserName == userName)
                .Include(t => t.Messages)
                .Include(t => t.User).ToListAsync();
            return dbChats;
        }
        [HttpGet("getthechats/{id}")]
        public async Task<List<UserModel>> UserChats(string id)
        {
            List<UserModel> userChats = await context.Users
                .Include(u => u.Chats)
                .Include(t => t.Messages).ToListAsync();
            return userChats;
        }

        //Get User by ID
        [HttpGet("get/{userName}")]
        public async Task<ActionResult> GetUser(string userName)
        {
            UserModel currentuser = await userManager.FindByNameAsync(userName); //gets current user using recieved ID
            if (currentuser != null)
                return Ok(currentuser);
            
            return BadRequest(currentuser);
        }

        [HttpGet("getbyname/{userName}")]
        public async Task<ActionResult> GetUserByName(string userName)
        {
            UserModel foundUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (foundUser != null)
            {
                return Ok(foundUser);
            }
            
            return BadRequest("Failed");
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
                chats = await db.Chats.Where(c => c.UserName == userId).ToListAsync();

                foreach (var chat in chats)
                {
                    CurrentUser.Chats.Add(chat);
                }

                await db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("getfullinclude/{userName}")]
        public async Task<UserModel> GetUserWithLists(string userName)
        {
            var result = context.Users
                .Include(c => c.Chats)
                .Include(m => m.Messages);
               
                return await result.SingleOrDefaultAsync(x => x.UserName == userName);
            
        }

        [HttpPost("chatrequest/{id}")]
        public async Task<IActionResult> RequestChat(string userName, int id)
        {
            await using (var db = context)
            {
                var user = await GetUserWithLists(userName);
                if (user != null)
                {
                    var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == id); //finds chat using id recieved
                    if (chat != null)
                    {
                        user.Chats.Add(chat);
                        return Ok(user);
                    }

                    return BadRequest(chat);
                }
                return BadRequest(user);
                
            }
        }
    }
}