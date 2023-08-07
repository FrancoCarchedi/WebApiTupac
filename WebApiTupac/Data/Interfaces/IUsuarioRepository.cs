using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task ResetPassword(string username, string newPasword);
    }
}
