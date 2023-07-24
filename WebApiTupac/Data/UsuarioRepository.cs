using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetUsuarioByID(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task InsertUsuario(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDTO);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuario(int usuarioId)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(usuarioId);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsuario(int id, UsuarioDTO usuarioDTO)
        {
            var existe = await _context.Usuarios.AnyAsync(x => x.UsuarioId == id);
            if (existe)
            {
                _context.Update(usuarioDTO);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
