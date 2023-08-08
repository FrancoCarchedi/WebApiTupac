using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> UsuarioExists(int usuarioId);
        Task ResetPassword(string username, string newPasword);
    }
}
