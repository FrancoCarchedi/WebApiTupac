using WebApiTupac.Entities;

namespace WebApiTupac.Data.Interfaces
{
    public interface ICursadaRepository : IRepository<Cursada>
    {
        Task<IEnumerable<Cursada>> GetAllByUsuario(string UsuarioId);
        Task<bool> CursadaExists(string UsuarioId, string MateriaId);
    }
}
