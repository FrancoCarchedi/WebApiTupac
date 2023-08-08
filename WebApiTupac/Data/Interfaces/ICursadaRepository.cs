using WebApiTupac.Entities;

namespace WebApiTupac.Data.Interfaces
{
    public interface ICursadaRepository : IRepository<Cursada>
    {
        Task<IEnumerable<Cursada>> GetAllByUsuario(int UsuarioId);
        Task<bool> CursadaExists(int UsuarioId, int MateriaId);
    }
}
