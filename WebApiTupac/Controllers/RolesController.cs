using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("/api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRolRepository _rolesRepository;

        public RolesController(IRolRepository rolesRepository) {
            
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> Get()
        {
            var roles = await _rolesRepository.GetAll();

            return Ok(roles);
        }

        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<List<IdentityRole>>> GetRolesByUsuario(string id)
        {
            try
            {
                var roles = await _rolesRepository.GetRolesByUsuario(id);

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<IdentityRole>> Insert([FromBody] IdentityRole role)
        {
            var exist = await _rolesRepository.GetRoleByName(role.Name);
            if (exist == null) {
                await _rolesRepository.Insert(role);
                return Ok($"Se ha creado el rol '{role.Name}'");
            }


            return BadRequest($"Ya existe el rol '{exist.Name}'");
        }
    }
}
