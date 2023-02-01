﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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

        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager,
            DataContext context){
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        //Get User by ID
        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetUser(string id){
            UserModel currentuser = await userManager.FindByIdAsync(id); //gets current user using recieved ID
            if (currentuser != null)
                return Ok(currentuser);
            return BadRequest(currentuser);
        }

        [Authorize]
        [HttpGet("getcurrent")] //get current user from claim
        public async Task<ActionResult> GetCurrentUser(){
            try{
                var uid = User.FindFirst(ClaimTypes.Name)?.Value; //Finds user claim

                UserModel? user = await context.Users //finds user that matches claim
                    .FirstOrDefaultAsync(u => u.UserName == uid);
                UserModel dtoUser = new UserModel(){
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    NormalizedUserName = user.NormalizedUserName
                };
                return Ok(dtoUser); //returns found user 
            }
            catch (Exception e){
                Console.WriteLine("GetCurrentUser() failed!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return BadRequest(e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel user){
            if (ModelState.IsValid) //Checks if object is valid
            {
                var result = await signInManager
                    .PasswordSignInAsync(user.Username, user.Password, true, false); // signs in user.
                if (result.Succeeded) // checks if signin succeded
                {
                    UserModel currentUser =
                        await signInManager.UserManager.FindByNameAsync(user.Username); // gets current user by username
                    var claims = new List<Claim> //sets up userid claim
                    {
                        new("userid", currentUser.Id),
                        new(ClaimTypes.Name, currentUser.UserName)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

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
        public async Task<IActionResult> SignupUser(RegisterModel registerModel){
            if (ModelState.IsValid){
                var checkUser =
                    await userManager.FindByNameAsync(registerModel.Username); //Checks if user already exists in DB
                if (checkUser != null)
                    return BadRequest("User Already exists!!");

                var user = new UserModel() // sets usermodel props to registerModel props
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };

                var result =
                    await userManager.CreateAsync(user,
                        registerModel
                            .Password); // Creates user account sets after usermodel and registermodels password

                if (result.Succeeded) // Checks if createasync succeded
                {
                    string role = "Member";
                    LoginModel login = registerModel;
                    await userManager.AddToRoleAsync(user, role); // sets register'd users role to "Member"
                    await LoginUser(login); //Signs in user after registering
                    return Ok(result);
                }

                foreach (var error in result.Errors){
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync(){
            await HttpContext.SignOutAsync();
            await signInManager.SignOutAsync();
            return Ok("Successful sign out!");
        }


        // Update email for user
        [HttpPut("edituser")]
        public async Task<IActionResult> UpdateUsersMail(UserModel updateModel){
            if (ModelState.IsValid){
                var checkUser =
                    await signInManager.UserManager.FindByIdAsync(updateModel.Id); // Gets current user using ID

                if (checkUser == null){
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
        public async Task<ActionResult<List<MessageModel>>> AddMessagesToUser(MessageModel messageModel){
            var CurrentUser = await userManager.FindByIdAsync(messageModel.UserId);
            List<MessageModel> messages = new();
            using (var db = context){
                messages = await db.Messages.Where(m => m.UserId == messageModel.UserId).ToListAsync();

                foreach (var message in messages){
                    CurrentUser.Messages.Add(message);
                }

                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("ChatList")]
        public async Task<ActionResult<List<ChatModel>>> AddChatsToUser(ChatModel chatModel){
            var CurrentUser = await userManager.FindByIdAsync(chatModel.UserId);

            List<ChatModel> chats = new();

            using (var db = context){
                chats = await db.Chats.Where(m => m.UserId == chatModel.UserId).ToListAsync();

                foreach (var chat in chats){
                    CurrentUser.Chats.Add(chat);
                }

                db.SaveChanges();
            }

            return Ok();
        }
        /*ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        }, "auth");
        ClaimsPrincipal claims = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(claims);*/
    }
}