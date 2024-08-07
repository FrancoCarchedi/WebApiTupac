using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTupac.Data;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;

namespace WebApiTupac.Services
{
    public interface IAuthtService
    {
        Task<string> GenerateJwtTokenAsync(string username, Usuario user);
        public ClaimsPrincipal GetPrincipalFromToken(string token);
    }
    public class AuthtService : IAuthtService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IRolRepository _rolesRepository;

        public AuthtService(IConfiguration configuration, ApplicationDbContext context, IRolRepository rolesRepository)
        {
            _configuration = configuration;
            _context = context;
            _rolesRepository = rolesRepository;
        }

        public async Task<string> GenerateJwtTokenAsync(string username, Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id),
            };

            var userRoles = await _rolesRepository.GetRolesByUsuario(user.Id);

            // Agrega los roles del usuario a las reclamaciones
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("roles", role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
