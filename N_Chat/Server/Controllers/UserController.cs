using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity;
using N_Chat.Shared.dto;
//using AutoMapper;

namespace N_Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

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
                if (checkUser.UserName == registerModel.Username)
                    return BadRequest("User Already exists!!");

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
        }

    }
}
