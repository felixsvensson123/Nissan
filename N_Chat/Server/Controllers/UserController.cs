using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity;
using N_Chat.Shared.dto;
using AutoMapper;

namespace N_Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, DataContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel user)
        {
            if (ModelState.IsValid) //Checks if object is valid
            {
                var result = await signInManager.PasswordSignInAsync(user.Username, user.Password, false, false); // signs in user.
                if (result.Succeeded) // checks if signin succeded
                {
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
                var checkUser = await userManager.FindByNameAsync(registerModel.Username); //Checks if user already exists in DB
                if (checkUser != null)
                {
                    return BadRequest();
                }

                var user = new UserModel() // sets usermodel props to registerModel props
                {
                 UserName = registerModel.Username,
                 Email = registerModel.Email
                };
                
                var result = await userManager.CreateAsync(user, registerModel.Password); // Creates user account sets after usermodel and registermodels password

                if (result.Succeeded) // Checks if createasync succeded
                {
                    await signInManager.UserManager.AddToRoleAsync(user, "Member"); // sets register'd users role to "Member"
                    await signInManager.PasswordSignInAsync(user, registerModel.Password, false, false); //"Signs in user after registering"
                    return Ok(result);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }// Update email for user
        [HttpPut("edituser")]
        public async Task<IActionResult> UpdateUsersMail(UserModel updateModel)
        {
            if (ModelState.IsValid)
            { 

            var checkUser = await signInManager.UserManager.FindByIdAsync(updateModel.Id);

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
            using (var db = context)
            {
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
        public async Task<ActionResult<List<ChatModel>>> GetChat (ChatModel chatModel)
        {
            var CurrentUser = await userManager.FindByIdAsync(chatModel.UserId);

            List<ChatModel> chats = new();

        //for each loop
        //context.add alla items i listan till context.Users
        //se till så att meddelandet hamnar på rätt user(Where kan kanske fungera)
            using(var db = context) 
            { 
                chats = await db.Chats.Where(m => m.UserId == chatModel.UserId).ToListAsync();

                foreach (var chat in chats)
                {
                    CurrentUser.Chats.Add(chat);   

        //<ActionResult<MessageModel>> och en ChatModel [HttpPost]
        // List<MessageModel> messageList = context.Messages.FirstOrDefaultAsync(x => x.UserId == updateModel.Id).ToListAsync();
        // context add till databasen       Databas  Table       query for table
                }

                db.SaveChanges();
            
            }

            return Ok();

        }

    }
}
