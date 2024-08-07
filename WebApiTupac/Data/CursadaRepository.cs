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
            var cursadas = await _context.Cursadas
                .Include(d => d.Docente)
                .Include(a => a.Usuario)
                .Include(m => m.Materia)
                .ToListAsync();
            return cursadas;
        }
        public async Task<IEnumerable<Cursada>> GetAllByUsuario(string UsuarioId)
        {
            var cursadasByUsuario = await _context.Cursadas.Where(u => u.UsuarioId == UsuarioId).Include(u => u.Usuario).Include(u => u.Docente).Include(m => m.Materia).ToListAsync();
            return cursadasByUsuario;
        }

        public async Task<IEnumerable<Cursada>> GetAllByDocente(string docenteId)
        {
            var cursadasByDocente = await _context.Cursadas.Where(u => u.DocenteId == docenteId).Include(u => u.Docente).Include(u => u.Usuario).Include(m => m.Materia).ToListAsync();
            return cursadasByDocente;
        }
        public async Task<Cursada> GetById(string id)
        {
            var cursada = await _context.Cursadas.Include(u => u.Usuario).Include(m => m.Materia).FirstOrDefaultAsync(c => c.CursadaId.ToString() == id);
            return cursada;
        }

        public async Task<bool> CursadaExists(string usuarioId, string materiaId)
        {
            return await _context.Cursadas.AnyAsync(c => c.UsuarioId == usuarioId && c.MateriaId.ToString() == materiaId);
        }

        public async Task Insert(Cursada cursada)
        {
            _context.Cursadas.Add(cursada);
            await _context.SaveChangesAsync();
        }

        public async Task Update(string id, Cursada cursada)
        {
            var existe = await _context.Cursadas.AnyAsync(x => x.CursadaId.ToString() == id);
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
        public async Task DeleteById(string id)
        {
            Cursada cursada = await _context.Cursadas.FindAsync(id);
            _context.Cursadas.Remove(cursada);
            await _context.SaveChangesAsync();
        }
    }
}
