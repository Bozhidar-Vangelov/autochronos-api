using AutoChronos.API.Dtos;
using AutoChronos.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoChronos.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(UserManager<User> userManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("User registered successfully.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
