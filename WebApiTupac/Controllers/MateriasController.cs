using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Controllers
{
    [Route("api/materias")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepository _materiasRepository;
        private readonly ICarreraRepository _carrerasRepository;
        private readonly IMapper _mapper;
        public MateriasController(IMateriaRepository materiaRepository, ICarreraRepository carreraRepository, IMapper mapper) 
        {
            _materiasRepository = materiaRepository;
            _carrerasRepository = carreraRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetById(string id)
        {
            var materia = await _materiasRepository.GetById(id);
            if (materia == null)
            {
                return NotFound("La materia no se ha encontrado.");
            }
            var materiaDTO = _mapper.Map<Materia>(materia);
            return Ok(materiaDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<Materia>>> Get()
        {
            var materias = await _materiasRepository.GetAll();
            var materiasDTO = _mapper.Map<IEnumerable<MateriaDTO>>(materias);
            return Ok(materias);
        }

        [HttpGet("/api/carreras/{carreraId}/materias")]
        public async Task<ActionResult<List<MateriaDTO>>> GetByCarrera(string carreraId)
        {
            var materias = await _materiasRepository.GetByCarrera(carreraId);
            if (materias.IsNullOrEmpty())
            {
                return BadRequest("La carrera especificada no existe.");
            }
            var materiasDTO = _mapper.Map<IEnumerable<Materia>>(materias);
            return Ok(materiasDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromForm] MateriaCreacionDTO materiaCreacionDTO)
        {
            var carrera = await _carrerasRepository.GetById(materiaCreacionDTO.CarreraId);

            if (carrera == null)
            {
                return BadRequest("La carrera especificada no existe.");
            }
            var materia = _mapper.Map<Materia>(materiaCreacionDTO);
            await _materiasRepository.Insert(materia);
            return Ok("La materia se ha insertado correctamente.");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([FromForm] MateriaActualizacionDTO materiaActualizacionDTO, string id)
        {

            var existe = await _materiasRepository.GetById(id);
            if (existe == null)
            {
                return NotFound("La materia a editar no se encuentra");
            }

            _mapper.Map(materiaActualizacionDTO, existe);

            await _materiasRepository.Update(id, existe);
            return Ok("Se han actualizado los datos de la materia");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await _materiasRepository.GetById(id);
            if (existe == null)
            {
                return BadRequest("La materia a eliminar no existe.");
            }
            await _materiasRepository.DeleteById(id);
            return Ok("Se ha eliminado la materia.");
        }
    }
}
