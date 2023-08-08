using AutoMapper;
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
        private readonly IMapper _mapper;
        public UsuariosController(IUsuarioRepository usuariosRepository, IMapper mapper)
        {
            _usuariosRepository = usuariosRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuariosRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound("El usuario no se ha encontrado");
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuariosDTO>>> Get()
        {
            var usuarios = await _usuariosRepository.GetAll();
            var usuariosDTO = _mapper.Map<IEnumerable<UsuariosDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioCreacionDTO);
            await _usuariosRepository.Insert(usuario);
            return Ok("El usuario se ha insertado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UsuarioActualizacionDTO usuarioActualizacionDTO)
        {
            var usuario = await _usuariosRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound("El usuario a editar no se encuentra");
            }

            //usuario.Nombre = usuarioActualizacionDTO.Nombre ?? usuario.Nombre;
            //usuario.Apellido = usuarioActualizacionDTO.Apellido ?? usuario.Apellido;
            //usuario.NombreUsuario = usuarioActualizacionDTO.NombreUsuario ?? usuario.NombreUsuario;
            //usuario.Contrasena = usuarioActualizacionDTO.Contrasena ?? usuario.Contrasena;
            //usuario.Email = usuarioActualizacionDTO.Email ?? usuario.Email;

            _mapper.Map(usuarioActualizacionDTO, usuario);

            await _usuariosRepository.Update(usuario.UsuarioId, usuario);
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
