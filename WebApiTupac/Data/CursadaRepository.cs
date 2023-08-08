using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;

namespace WebApiTupac.Data
{
    public class CursadaRepository : ICursadaRepository
    {
        private readonly ApplicationDbContext _context;
        public CursadaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cursada>> GetAll()
        {
            var cursadas = await _context.Cursadas.ToListAsync();
            return cursadas;
        }
        public async Task<IEnumerable<Cursada>> GetAllByUsuario(int UsuarioId)
        {
            var cursadasByUsuario = await _context.Cursadas.Where(u => u.UsuarioId == UsuarioId).Include(u => u.Usuario).Include(m => m.Materia).ToListAsync();
            return cursadasByUsuario;
        }
        public async Task<Cursada> GetById(int id)
        {
            var cursada = await _context.Cursadas.Include(u => u.Usuario).Include(m => m.Materia).FirstOrDefaultAsync(c => c.CursadaId == id);
            return cursada;
        }

        public async Task<bool> CursadaExists(int usuarioId, int materiaId)
        {
            return await _context.Cursadas.AnyAsync(c => c.UsuarioId == usuarioId && c.MateriaId == materiaId);
        }

        public async Task Insert(Cursada cursada)
        {
            _context.Cursadas.Add(cursada);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Cursada cursada)
        {
            var existe = await _context.Cursadas.AnyAsync(x => x.CursadaId == id);
            if (existe)
            {
                _context.Update(cursada);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("La cursada no existe.");
            }
        }
        public async Task DeleteById(int id)
        {
            Cursada cursada = await _context.Cursadas.FindAsync(id);
            _context.Cursadas.Remove(cursada);
            await _context.SaveChangesAsync();
        }
    }
}
