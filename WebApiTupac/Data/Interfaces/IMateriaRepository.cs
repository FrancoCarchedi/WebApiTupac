using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface IMateriaRepository : IRepository<MateriaDTO>
    {
        Task<IEnumerable<MateriaDTO>> GetByCarrera(int carreraId);
    }
}
