using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data;
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
        public CarrerasController(ICarreraRepository carreraRepository)
        {
            _carrerasRepository = carreraRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarreraDTO>> GetById(int id)
        {
            var carrera = await _carrerasRepository.GetById(id);
            if (carrera == null)
            {
                return NotFound("La carrera no se ha encontrado");
            }
            return Ok(carrera);
        }

        [HttpGet]
        public async Task<ActionResult<List<CarreraDTO>>> Get()
        {
            var carreras = await _carrerasRepository.GetAll();
            return Ok(carreras);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CarreraDTO carreraDTO)
        {
            await _carrerasRepository.Insert(carreraDTO);
            return Ok("La carrera se ha insertado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int carreraId, CarreraDTO carrera)
        {
            var existe = await _carrerasRepository.GetById(carreraId);
            if (existe == null)
            {
                return NotFound("La carrera a editar no se encuentra");
            }
            await _carrerasRepository.Update(carreraId, carrera);
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
