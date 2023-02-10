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
        //[Authorize (AuthenticationSchemes = ClaimsIdentity.DefaultNameClaimType)] // testing different auths for requests dont touch
        [HttpGet("{userName}")]
        public async Task<UserModel> GetUserWithIncludes (string userName) // rör ej funkar
        {
            var result = context.Users
                .Include(c => c.Chats)
                .Include(m => m.Messages);
               
            return await result.SingleOrDefaultAsync(x => x.UserName == userName);
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("chats")] 
        public async Task<IEnumerable<ChatModel>> GetIncludedUserChats()
        {
            List<ChatModel> dbChats = await context.Chats
                .Include(u => u.Users)
                .Include(t => t.Messages)
                .Include(t => t.User).ToListAsync();
            return dbChats;
        }
        
        [Authorize]
        [HttpGet("getcurrent")] //get current user from claim
        public async Task<ActionResult> GetCurrentUser()
        {
            var claimValue = User.FindFirst(ClaimTypes.Name)?.Value; //Finds user claim

            UserModel? user = await context.Users //finds user that matches claim
                .FirstOrDefaultAsync(u => u.UserName == claimValue);
            
            UserModel dtoUser = new UserModel() //ska fixa med denna sen rör inget!!! //
            {
                Id = user.Id,
                UserName = user.UserName,               //ska fixa med denna sen rör inget!!! //
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName            //ska fixa med denna sen rör inget!!! //
            };
            return Ok(user); //returns found user
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel user) // rör ej funkar
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager
                    .PasswordSignInAsync(user.Username, user.Password, true, false); // signs in user.
                if (result.Succeeded)
                {
                    UserModel currentUser =
                        await signInManager.UserManager.FindByNameAsync(user.Username); // gets current user by username
                    var claims = new List<Claim> //sets up user claim
                    {
                        new(ClaimTypes.Name,
                            currentUser.UserName) 
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
        public async Task<IActionResult> SignupUser(RegisterModel registerModel) // funkar rör ej!
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

        
        [HttpPost("chatrequest/{id}")] // not finished
        public async Task<IActionResult> RequestChat(string userName, int id)
        {
            await using (var db = context)
            {
                var user = await GetUserWithIncludes(userName);
                if (user != null)
                {
                    var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == id); //finds chat using id recieved
                    if (chat != null)
                    {
                        user.Chats.Add(chat);
                        return Ok(user);
                    }

                    return BadRequest("Error: " + chat);
                }
                return BadRequest("Error:" + user);
                
            }
        }
        
        
        
        
        
        
        
        /*
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
        */
    }
}