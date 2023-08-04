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

        public async Task<IEnumerable<UsuarioDTO>> GetAll()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }
        public async Task<UsuarioDTO> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }
        public async Task Insert(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDTO);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, UsuarioDTO usuarioDTO)
        {
            var existe = await _context.Usuarios.AnyAsync(x => x.UsuarioId == id);
            if (existe)
            {
                _context.Update(usuarioDTO);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
        }
        public async Task DeleteById(int id)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPassword(string username, string newPasword)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.NombreUsuario.Equals(username));

            if (usuario != null)
            {
                // Actualizar la contraseña del usuario
                usuario.Contrasena = newPasword;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
        }
    }
}
