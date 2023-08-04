using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<UsuarioDTO>
    {
        Task ResetPassword(string username, string newPasword);
    }
}
