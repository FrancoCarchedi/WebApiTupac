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
            var materias = await _context.Materias.Include(c => c.Carrera).ToListAsync();
            return materias;
        }

        public async Task<Materia> GetById(string id)
        {
            var materia = await _context.Materias
                .Include(c => c.Carrera)
                .FirstOrDefaultAsync(m => m.MateriaId.ToString() == id);

            return materia;
        }
        public async Task<bool> MateriaExists(string materiaId)
        {
            return await _context.Materias.AnyAsync(m => m.MateriaId.ToString() == materiaId);
        }

        public async Task<IEnumerable<Materia>> GetByCarrera(string carreraId)
        {
            var materias = await _context.Materias.Where(m => m.CarreraId.ToString() == carreraId).ToListAsync();
            return materias;
        }

        public async Task Insert(Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
        }

        public async Task Update(string id, Materia materia)
        {
            var existe = await _context.Materias.AnyAsync(x => x.MateriaId.ToString() == id);
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
        public async Task DeleteById(string id)
        {
            Materia materia = await _context.Materias.FindAsync(id);
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
        }
    }
}
