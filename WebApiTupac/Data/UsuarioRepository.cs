using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;

namespace WebApiTupac.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioRepository(ApplicationDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<string>> GetUserRoles(Usuario usuario)
        {
            var userRoles = await _context.UserRoles.Where(u => u.UserId == usuario.Id).ToListAsync();

            var roles = new List<string>();

            foreach (var role in userRoles) {
                var userRole = await _context.Roles.Where(r => r.Id == role.RoleId).FirstOrDefaultAsync();
                roles.Add(userRole.Name);
            }

            return roles;

        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return usuarios;
        }
        public async Task<Usuario> GetById(string id)
        {
            var usuario = await _context.Usuarios.Include(c => c.CursadasComoAlumno).
                ThenInclude(m => m.Materia)
                .FirstOrDefaultAsync(u => u.Id == id);

            return usuario;
        }

        public async Task<Usuario> GetByMail(string mail)
        {
            var usuario = await _context.Usuarios.Include(c => c.CursadasComoAlumno).
                ThenInclude(m => m.Materia)
                .FirstOrDefaultAsync(u => u.Email == mail);

            return usuario;
        }


        public async Task Insert(Usuario usuario)
        {
            var result = await _userManager.CreateAsync(usuario);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"No se pudo crear el usuario: {errors}");
            }

            //await SetPassword(usuario, usuario.PasswordHash);
        }

        public async Task Update(string id, Usuario usuario)
        {
            var existe = await _context.Usuarios.AnyAsync(x => x.Id == id);
            if (existe)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
        }
        public async Task DeleteById(string id)
        {
            Usuario usuario = await GetById(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task SetRoles(Usuario usuario, ICollection<string> roles)
        {
            if (usuario == null)
            {
                throw new Exception("El usuario no es válido");
            }

            await _userManager.AddToRolesAsync(usuario, roles);
        }

        //public async Task SetPassword(Usuario usuario, string password)
        //{
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
        //    var result = await _userManager.ResetPasswordAsync(usuario, token, password);
        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        throw new Exception($"No se pudo establecer la contraseña: {errors}");
        //    }
        //}

        public async Task ResetPassword(string username, string newPasword)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.UserName.Equals(username));

            if (usuario != null)
            {
                // Actualizar la contraseña del usuario
                usuario.PasswordHash = newPasword;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El usuario ingresado no es correcto.");
            }
        }
    }
}
