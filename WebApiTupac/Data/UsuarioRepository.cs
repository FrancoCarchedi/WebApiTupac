using Microsoft.EntityFrameworkCore;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }
        public async Task<Usuario> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }
        public async Task<bool> UsuarioExists(int usuarioId)
        {
            return await _context.Usuarios.AnyAsync(u => u.UsuarioId == usuarioId);
        }
        public async Task Insert(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Usuario usuario)
        {
            var existe = await _context.Usuarios.AnyAsync(x => x.UsuarioId == id);
            if (existe)
            {
                _context.Update(usuario);
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
                throw new Exception("El usuario ingresado no es correcto.");
            }
        }
    }
}
