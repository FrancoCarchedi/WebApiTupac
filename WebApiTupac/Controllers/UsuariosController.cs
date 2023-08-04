using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("/api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuariosRepository;
        public UsuariosController(IUsuarioRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _usuariosRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound("El usuario no se ha encontrado");
            }
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _usuariosRepository.GetAll();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] UsuarioDTO usuarioDTO)
        {
            await _usuariosRepository.Insert(usuarioDTO);
            return Ok("El usuario se ha insertado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int usuarioId, UsuarioDTO usuario)
        {
            var existe = await _usuariosRepository.GetById(usuarioId);
            if (existe == null)
            {
                return NotFound("El usuario a editar no se encuentra");
            }
            await _usuariosRepository.Update(usuarioId, usuario);
            return Ok("Se han actualizado los datos del usuario");
        }

        [HttpPut("{id:int}/updatePassword")]
        public async Task<ActionResult> UpdatePassword(PasswordUpdateDTO usuario)
        {
            await _usuariosRepository.ResetPassword(usuario.NombreUsuario, usuario.Contrasena);
            return Ok("Se actualizo la contraseña");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _usuariosRepository.DeleteById(id);
            return Ok("El usuario se eliminó correctamente");
        }
    }
}
