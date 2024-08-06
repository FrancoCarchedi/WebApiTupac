using Microsoft.AspNetCore.Identity;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetByMail(string mail);
        Task ResetPassword(string username, string newPasword);
        //Task SetPassword(Usuario usuario, string password);
        Task<IEnumerable<string>> GetUserRoles(Usuario usuario);
        Task SetRoles(Usuario usuario, ICollection<string> roles);
    }
}
