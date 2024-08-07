using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Services;

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
            return carreras;
        }

        public async Task<Carrera> GetById(string id)
        {
            var carrera = await _context.Carreras.Include(c => c.Materias).FirstOrDefaultAsync(c => c.CarreraId.ToString() == id);
            return carrera;
        }

        public async Task<Carrera> GetBySlug(string slug)
        {
            var carrera = await _context.Carreras.Include(c => c.Materias).FirstOrDefaultAsync(c => c.Slug == slug);
            return carrera;
        }

        public async Task Insert(Carrera carrera)
        {
            carrera.Slug = SlugHelper.GenerateSlug(carrera.Nombre);
            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();
        }

        public async Task Update(string id, Carrera carrera)
        {
            var existe = await _context.Carreras.AnyAsync(x => x.CarreraId.ToString() == id);
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

        public async Task DeleteById(string id)
        {
            Carrera carrera = await GetById(id);
            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();
        }
    }
}
