using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;

namespace WebApiTupac.Data
{
    public class CursadaRepository : ICursadaRepository
    {
        public Task<Cursada> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cursada>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Cursada entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, Cursada entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
