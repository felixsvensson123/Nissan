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

        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IMapper mapper;
        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel user)
        {
            if (ModelState.IsValid) //Checks if object is valid
            {
                var result = await signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);
                if (result.Succeeded)
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
                var user = mapper.Map<UserModel>(registerModel.Username);
                var result = await userManager.CreateAsync(user, registerModel.Password);

                await signInManager.UserManager.AddToRoleAsync(user, "Member");
                await signInManager.PasswordSignInAsync(user.UserName, registerModel.Password, false, false);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

    }
}
