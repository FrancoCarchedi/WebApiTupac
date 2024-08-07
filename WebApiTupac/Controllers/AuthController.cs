using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;
using WebApiTupac.Services;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IAuthtService _authService;

        public AuthController(IAuthtService authService, IUsuarioRepository usuariosRepository, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
            _usuariosRepository = usuariosRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == login.Username);
                var token = await _authService.GenerateJwtTokenAsync(login.Username, appUser);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("profile")]
        public async Task<IActionResult> GetUserFromToken([FromBody] TokenDTO token)
        {
            var principal = _authService.GetPrincipalFromToken(token.Token);
            if (principal is null)
            {
                return Unauthorized();
            }

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _usuariosRepository.GetUserRoles(user);

            var userProfile = new PerfilUsuarioDTO
            {
                Id = userId,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Username = user.UserName,
                Email = user.Email,
                Foto = user.Foto,
                Roles = userRoles
            };

            return Ok(userProfile);
        }
    }
}
