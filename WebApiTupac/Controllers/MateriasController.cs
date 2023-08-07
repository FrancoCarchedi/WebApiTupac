using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Controllers
{
    [Route("api/materias")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepository _materiasRepository;
        public MateriasController(IMateriaRepository materiaRepository) 
        {
            _materiasRepository = materiaRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MateriaDTO>> GetById(int id)
        {
            var materia = await _materiasRepository.GetById(id);
            if (materia == null)
            {
                return NotFound("La materia no se ha encontrado");
            }
            return Ok(materia);
        }

        [HttpGet]
        public async Task<ActionResult<List<MateriaDTO>>> Get()
        {
            var materias = await _materiasRepository.GetAll();
            return Ok(materias);
        }

        [HttpGet("/api/carreras/{carreraId}/materias")]
        public async Task<ActionResult<List<MateriaDTO>>> GetByCarrera(int carreraId)
        {
            var materias = await _materiasRepository.GetByCarrera(carreraId);
            return Ok(materias);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] MateriaDTO materiaDTO)
        {
            await _materiasRepository.Insert(materiaDTO);
            return Ok("La materia se ha insertado correctamente.");
        }
    }
}
