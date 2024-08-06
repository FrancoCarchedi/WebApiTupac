using WebApiTupac.Entities;

namespace WebApiTupac.Data.Interfaces
{
    public interface IMateriaRepository : IRepository<Materia>
    {
        Task<IEnumerable<Materia>> GetByCarrera(string carreraId);
    }
}
