using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;

namespace WebApiTupac.Data
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly ApplicationDbContext _context;
        public MateriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Materia>> GetAll()
        {
            var materias = await _context.Materias.ToListAsync();
            return materias;
        }

        public async Task<Materia> GetById(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            return materia;
        }

        public async Task<IEnumerable<Materia>> GetByCarrera(int carreraId)
        {
            var materias = await _context.Materias.Where(m => m.CarreraId == carreraId).ToListAsync();
            return materias;
        }

        public async Task Insert(Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Materia materia)
        {
            var existe = await _context.Materias.AnyAsync(x => x.MateriaId == id);
            if (existe)
            {
                _context.Update(materia);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("La materia no existe.");
            }
        }
        public async Task DeleteById(int id)
        {
            Materia materia = await _context.Materias.FindAsync(id);
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
        }
    }
}
