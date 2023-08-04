using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarreraRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarreraDTO>> GetAll()
        {
            var carreras = await _context.Carreras.ToListAsync();
            return _mapper.Map<IEnumerable<CarreraDTO>>(carreras);
        }

        public async Task<CarreraDTO> GetById(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            return _mapper.Map<CarreraDTO>(carrera);
        }

        public async Task Insert(CarreraDTO carreraDTO)
        {
            Carrera carrera = _mapper.Map<Carrera>(carreraDTO);
            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, CarreraDTO carreraDTO)
        {
            var existe = await _context.Carreras.AnyAsync(x => x.CarreraId == id);
            if (existe)
            {
                _context.Update(carreraDTO);
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
