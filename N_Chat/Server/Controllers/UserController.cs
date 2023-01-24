using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity;
using N_Chat.Shared.dto;

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
                var result = await signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt") // Error Message if string is empty
            }
            return BadRequest(user);
        }
    }
}
