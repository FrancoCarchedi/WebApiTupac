using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuarios();
        Task<UsuarioDTO> GetUsuarioByID(int usuarioId);
        Task InsertUsuario(UsuarioDTO usuarioDTO);
        Task DeleteUsuario(int usuarioId);
        Task UpdateUsuario(int usuarioId, UsuarioDTO usuario);
        //void Save();
    }
}
