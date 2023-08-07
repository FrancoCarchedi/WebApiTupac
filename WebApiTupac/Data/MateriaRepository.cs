using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MateriaRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MateriaDTO>> GetAll()
        {
            var materias = await _context.Materias.ToListAsync();
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }

        public async Task<MateriaDTO> GetById(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<IEnumerable<MateriaDTO>> GetByCarrera(int carreraId)
        {
            var materias = await _context.Materias.Where(m => m.CarreraId == carreraId).ToListAsync();
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }

        public async Task Insert(MateriaDTO materiaDTO)
        {
            Materia materia = _mapper.Map<Materia>(materiaDTO);
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, MateriaDTO materiaDTO)
        {
            var existe = await _context.Materias.AnyAsync(x => x.MateriaId == id);
            if (existe)
            {
                _context.Update(materiaDTO);
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
