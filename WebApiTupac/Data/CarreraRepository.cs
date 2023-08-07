using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;

namespace WebApiTupac.Data
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly ApplicationDbContext _context;

        public CarreraRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Carrera>> GetAll()
        {
            var carreras = await _context.Carreras.Include(c => c.Materias).ToListAsync();
            //return _mapper.Map<IEnumerable<CarreraDTO>>(carreras);
            return carreras;
        }

        public async Task<Carrera> GetById(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            return carrera;
        }

        public async Task Insert(Carrera carrera)
        {

            //Carrera carrera = _mapper.Map<Carrera>(carreraDTO);

            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Carrera carrera)
        {
            var existe = await _context.Carreras.AnyAsync(x => x.CarreraId == id);
            if (existe)
            {
                _context.Update(carrera);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("La carrera no existe.");
            }
        }

        public async Task DeleteById(int id)
        {
            Carrera carrera = await _context.Carreras.FindAsync(id);
            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();
        }
    }
}
