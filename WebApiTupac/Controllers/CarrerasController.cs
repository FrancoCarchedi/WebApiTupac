using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;
using WebApiTupac.Services;

namespace WebApiTupac.Controllers
{
    [ApiController]
    [Route("api/carreras")]
    public class CarrerasController : ControllerBase
    {
        private readonly ICarreraRepository _carrerasRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public CarrerasController(ICarreraRepository carreraRepository, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _carrerasRepository = carreraRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarreraDTO>> GetById(string id)
        {
            var carrera = await _carrerasRepository.GetById(id);
            if (carrera == null)
            {
                return NotFound("La carrera no se ha encontrado");
            }
            //var carreraDTO = _mapper.Map<Carrera>(carrera);
            return Ok(carrera);
        }

        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<CarreraDTO>> GetBySlug(string slug)
        {
            var carrera = await _carrerasRepository.GetBySlug(slug);
            if (carrera == null)
            {
                return NotFound("La carrera no se ha encontrado");
            }
            //var carreraDTO = _mapper.Map<Carrera>(carrera);
            return Ok(carrera);
        }

        [HttpGet]
        public async Task<ActionResult<List<CarreraDTO>>> Get()
        {
            var carreras = await _carrerasRepository.GetAll();
            var carrerasDTO = _mapper.Map<IEnumerable<CarreraDTO>>(carreras);
            return Ok(carrerasDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromForm] CarreraCreacionDTO carreraCreacionDTO, IFormFile portrait, IFormFile plan)
        {
            if (portrait != null && portrait.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(portrait);
                carreraCreacionDTO.Portada = imageUrl;
            }

            if (plan != null && plan.Length > 0)
            {
                var pdfUrl = await _cloudinaryService.UploadPdfAsync(plan);
                carreraCreacionDTO.Plan = pdfUrl;
            }

            Carrera carrera = _mapper.Map<Carrera>(carreraCreacionDTO);
            await _carrerasRepository.Insert(carrera);
            return Ok("La carrera se ha insertado correctamente.");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([FromForm] CarreraActualizacionDTO carreraDTO, string id)
        {
            var existe = await _carrerasRepository.GetById(id);
            if (existe == null)
            {
                return NotFound("La carrera a editar no se encuentra");
            }

            //existe.Nombre = carreraDTO.Nombre;
            //existe.Duracion = carreraDTO.Duracion;
            _mapper.Map(carreraDTO, existe);

            await _carrerasRepository.Update(id, existe);
            return Ok("Se han actualizado los datos de la carrera");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _carrerasRepository.DeleteById(id);
            return Ok("La carrera se eliminó correctamente");
        }
    }
}
