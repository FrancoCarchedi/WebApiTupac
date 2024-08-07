using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data.Interfaces
{
    public interface ICarreraRepository : IRepository<Carrera>
    {
        Task<Carrera> GetBySlug(string slug);
    }
}
