using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;
using WebApiTupac.Services;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IAuthtService _authService;

        public AuthController(IAuthtService authService, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == login.Username);
                var token = await _authService.GenerateJwtTokenAsync(login.Username, appUser);
                return Ok(new { Token = "Bearer " + token });
            }

            return Unauthorized();
        }
    }
}
