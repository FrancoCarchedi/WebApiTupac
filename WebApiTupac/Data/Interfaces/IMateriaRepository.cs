using WebApiTupac.Entities;

namespace WebApiTupac.Data.Interfaces
{
    public interface IMateriaRepository : IRepository<Materia>
    {
        Task<bool> MateriaExists(int materiaId);
        Task<IEnumerable<Materia>> GetByCarrera(int carreraId);
    }
}
