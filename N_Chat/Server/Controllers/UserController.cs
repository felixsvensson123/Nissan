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
        [Authorize(Roles = "Admin")]
        [HttpGet("chats")]
        public async Task<ICollection<UserModel>> getAllUsers()
        {
            ICollection<UserModel> users = await context.Users
                .Include(u => u.Chats)
                .Include(t => t.Messages)
                .ToListAsync();
            return users;
        }
        [HttpGet("getbyname/{userName}")]
        public async Task<ActionResult> GetUserByName(string userName)
        {
            UserModel foundUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return Ok(foundUser);
        }
        [HttpGet("{userName}")] // Gets user with chat and message list // made to reduce redundancy
        public async Task<UserModel> GetUserWithIncludes (string userName) // rör ej funkar
        {
            var result = context.Users
                .Include(u => u.Chats)
                .ThenInclude(uc => uc.Chat)
                .ThenInclude(c => c.Messages)
                .AsSingleQuery();

            return await result.SingleOrDefaultAsync(x => x.UserName == userName);
        }
        
        [Authorize]
        [HttpGet("getcurrent")] //get current user from claims
        public async Task<ActionResult> GetCurrentUser()
        {
            var claimValue = User.FindFirst(ClaimTypes.Name)?.Value;
            if (claimValue == null)
                return NotFound("User Claim was not found" + claimValue);

            UserModel? user = await context.Users
                    .Include(u => u.Chats)
                    .ThenInclude(uc => uc.Chat)
                    .ThenInclude(c => c.Messages)
                    .FirstOrDefaultAsync(u => u.UserName == claimValue);
            
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel user) // rör ej funkar
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await signInManager
                .PasswordSignInAsync(user.Username, user.Password, true, false); // signs in user.

            if (result.Succeeded)
            {
                var currentUser =
                    await signInManager.UserManager.FindByNameAsync(user.Username);
                
                var claims = new List<Claim> //sets up user claim
                {
                    new(ClaimTypes.Name,
                        currentUser.UserName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync( //sets claims principals and signs in user 
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Ok(result);
            }
            
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt"); // Error Message if string is empty
            return BadRequest(ModelState);
            
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

        [HttpGet("{userId}/userchat/")]
        public async Task<IActionResult> GetUserChats(string userId)
        {
            var userChat =  context.UserChats
                .Where(x => x.UserId == userId)
                .Include(uc => uc.User)
                .ThenInclude(u => u.Chats);
            
            return Ok(userChat);
        }
        [HttpPost("chatrequest/{chatId}")] // adds user to chat
        public async Task<IActionResult> RequestChat(UserModel user, int chatId)
        {
            var userChat = new UserChat()
            {
                UserId = user.Id,
                ChatId = chatId
            };
            
            if (userChat.ChatId == 0)
                return BadRequest("Error: " + userChat);
            
            await context.UserChats.AddAsync(userChat);
            await context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetUserWithIncludes), new { user.UserName, chatId }, userChat);
            
        }
        
        [HttpPost("newmessage")] // posts user message into user table -> message list
        public async Task<IActionResult> PostUserMessage(MessageModel messageModel)
        {
            await using (var db = context)
            {
                var user = await GetUserWithIncludes(User.Identity.Name);
                if (user.UserName != null)
                {
                    if (messageModel.Message != null)
                    {
                        user.Messages.Add(messageModel);
                        return Ok(user);
                    }

                    return BadRequest("Error: " + messageModel);
                    
                }
                
                return BadRequest("Error:" + user);
                
            }
        }
    }
}