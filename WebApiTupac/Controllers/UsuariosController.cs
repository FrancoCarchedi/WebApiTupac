using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;
using WebApiTupac.Services;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("/api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public UsuariosController(IUsuarioRepository usuariosRepository, UserManager<Usuario> userManager, ICloudinaryService cloudinaryService ,IMapper mapper)
        {
            _usuariosRepository = usuariosRepository;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(string id)
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
        public async Task<ActionResult> Insert([FromForm] UsuarioCreacionDTO usuarioCreacionDTO, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(file);
                usuarioCreacionDTO.Foto = imageUrl;
            }

            var usuario = _mapper.Map<Usuario>(usuarioCreacionDTO);

            await _userManager.CreateAsync(usuario, usuarioCreacionDTO.Password);
            await _usuariosRepository.SetRoles(usuario, usuarioCreacionDTO.Roles);

            return Ok("El usuario se ha insertado correctamente.");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(string id, [FromForm] UsuarioActualizacionDTO usuarioActualizacionDTO, [FromForm] IFormFile file)
        {
            var usuario = await _usuariosRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound("El usuario a editar no se encuentra");
            }

            _mapper.Map(usuarioActualizacionDTO, usuario);
            if (file != null && file.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(file);
                usuario.Foto = imageUrl;
            }

            await _usuariosRepository.Update(usuario.Id, usuario);
            return Ok("Se han actualizado los datos del usuario");
        }

        [HttpPut("{id}/updatePassword")]
        public async Task<ActionResult> UpdatePassword(PasswordUpdateDTO usuario)
        {
            await _usuariosRepository.ResetPassword(usuario.NombreUsuario, usuario.Contrasena);
            return Ok("Se actualizo la contraseña");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _usuariosRepository.DeleteById(id);
                return Ok("El usuario se eliminó correctamente");
            }
            catch (Exception ex) {
                throw new Exception($"Ocurrió un error al eliminar el usuario: {ex.Message}");
            }
            
        }
    }
}
