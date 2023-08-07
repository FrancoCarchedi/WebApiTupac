using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("api/carreras")]
    public class CarrerasController : ControllerBase
    {
        private readonly ICarreraRepository _carrerasRepository;
        private readonly IMapper _mapper;
        public CarrerasController(ICarreraRepository carreraRepository, IMapper mapper)
        {
            _carrerasRepository = carreraRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarreraDTO>> GetById(int id)
        {
            var carrera = await _carrerasRepository.GetById(id);
            if (carrera == null)
            {
                return NotFound("La carrera no se ha encontrado");
            }
            var carreraDTO = _mapper.Map<Carrera>(carrera);
            return Ok(carreraDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<CarreraDTO>>> Get()
        {
            var carreras = await _carrerasRepository.GetAll();
            var carrerasDTO = _mapper.Map<IEnumerable<CarreraDTO>>(carreras);
            return Ok(carrerasDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CarreraCreacionDTO carreraCreacionDTO)
        {
            Carrera carrera = _mapper.Map<Carrera>(carreraCreacionDTO);
            await _carrerasRepository.Insert(carrera);
            return Ok("La carrera se ha insertado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(CarreraCreacionDTO carreraDTO, int id)
        {
            var existe = await _carrerasRepository.GetById(id);
            if (existe == null)
            {
                return NotFound("La carrera a editar no se encuentra");
            }

            existe.Nombre = carreraDTO.Nombre;
            existe.Duracion = carreraDTO.Duracion;

            await _carrerasRepository.Update(id, existe);
            return Ok("Se han actualizado los datos de la carrera");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _carrerasRepository.DeleteById(id);
            return Ok("La carrera se eliminó correctamente");
        }
    }
}
