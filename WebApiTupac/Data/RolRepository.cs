using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;

namespace WebApiTupac.Data
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolRepository(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _context = context;
            _roleManager = roleManager;
        }
        async Task<IEnumerable<IdentityRole>> IRepository<IdentityRole>.GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        Task IRepository<IdentityRole>.DeleteById(string id)
        {
            throw new NotImplementedException();
        }


        async Task<IdentityRole> IRepository<IdentityRole>.GetById(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role;
        }
        public async Task<IdentityRole> GetRoleByName(string roleName)
        {
            var role = await _context.Roles.Where(r => r.Name == roleName).FirstOrDefaultAsync();
            return role;
        }

        async Task<IList<IdentityRole>> IRolRepository.GetRolesByUsuario(string usuarioId)
        {
            var user = await _context.Users.FindAsync(usuarioId) ?? throw new Exception($"No existe el usuario con id '{usuarioId}'");

            var userRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();
            
            var roles = new List<IdentityRole>();
            foreach (var userRole in userRoles)
            {
                var role = await _context.Roles.Where(r => r.Id == userRole.RoleId).FirstOrDefaultAsync();
                roles.Add(role);
            }

            return roles;
        }

        async Task IRepository<IdentityRole>.Insert(IdentityRole entity)
        {
            var role = await _roleManager.CreateAsync(entity);
            if (!role.Succeeded)
            {
                var errors = string.Join(", ", role.Errors.Select(e => e.Description));
                throw new Exception($"No se pudo crear el rol: {errors}");
            }
        }

        async Task IRepository<IdentityRole>.Update(string id, IdentityRole role)
        {
            var existe = await _context.Roles.AnyAsync(r => r.Id == id);
            if (existe)
            {
                _context.Update(role);
                await _context.SaveChangesAsync();
            } else
            {
                throw new Exception("El rol no es válido");
            }

        }

    }
}
