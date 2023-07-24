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

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _usuariosRepository.GetUsuarioByID(id);
            if (usuario == null)
            {
                return NotFound("El usuario no se ha encontrado");
            }
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _usuariosRepository.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] UsuarioDTO usuarioDTO)
        {
            await _usuariosRepository.InsertUsuario(usuarioDTO);
            return Ok("El usuario se ha insertado correctamente.");
        }

        [HttpPut]
        public async Task<ActionResult> Update(int usuarioId, UsuarioDTO usuario)
        {
            var existe = await _usuariosRepository.GetUsuarioByID(usuarioId);
            if (existe == null)
            {
                return NotFound("El usuario a editar no se encuentra");
            }
            await _usuariosRepository.UpdateUsuario(usuarioId, usuario);
            return Ok("Se han actualizado los datos del usuario");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _usuariosRepository.DeleteUsuario(id);
            return Ok("El usuario se eliminó correctamente");
        }
    }
}
