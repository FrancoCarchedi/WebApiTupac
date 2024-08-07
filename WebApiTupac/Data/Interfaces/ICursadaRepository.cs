using WebApiTupac.Entities;

namespace WebApiTupac.Data.Interfaces
{
    public interface ICursadaRepository : IRepository<Cursada>
    {
        Task<IEnumerable<Cursada>> GetAllByUsuario(string UsuarioId);
        Task<IEnumerable<Cursada>> GetAllByDocente(string docenteId);
        Task<bool> CursadaExists(string UsuarioId, string MateriaId);
    }
}
