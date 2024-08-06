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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MateriaDTO>> GetById(string id)
        {
            var materia = await _materiasRepository.GetById(id);
            if (materia == null)
            {
                return NotFound("La materia no se ha encontrado.");
            }
            var materiaDTO = _mapper.Map<MateriaDTO>(materia);
            return Ok(materiaDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<MateriaDTO>>> Get()
        {
            var materias = await _materiasRepository.GetAll();
            var materiasDTO = _mapper.Map<IEnumerable<MateriaDTO>>(materias);
            return Ok(materiasDTO);
        }

        [HttpGet("/api/carreras/{carreraId:int}/materias")]
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromBody] MateriaCreacionDTO materiaCreacionDTO, string id)
        {
            var materia = await _materiasRepository.GetById(id);

            if (materia == null)
            {
                return NotFound("La materia a editar no se encuentra.");
            }

            if (materiaCreacionDTO.CarreraId == materia.CarreraId.ToString())
            {
                materia.Nombre = materiaCreacionDTO.Nombre;
                materia.CarreraId = materia.CarreraId;

                await _materiasRepository.Update(materiaCreacionDTO.CarreraId, materia);
                return Ok("Se han actualizado los datos de la materia.");
            }

            return BadRequest("La carrera a la que hace referencia, no existe.");
        }

        [HttpDelete("{id:int}")]
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
