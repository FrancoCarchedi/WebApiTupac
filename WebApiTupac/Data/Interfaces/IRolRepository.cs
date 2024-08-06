using Microsoft.AspNetCore.Identity;

namespace WebApiTupac.Data.Interfaces
{
    public interface IRolRepository : IRepository<IdentityRole>
    {
        Task<IList<IdentityRole>> GetRolesByUsuario(string usuarioId);
        Task<IdentityRole> GetRoleByName(string roleName);
    }
}
