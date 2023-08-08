using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTupac.Data;
using WebApiTupac.Data.Interfaces;
using WebApiTupac.Entities;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Controllers
{
    [Route("api/cursadas")]
    [ApiController]
    public class CursadasController : ControllerBase
    {
        private readonly ICursadaRepository _cursadaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IMapper _mapper;
        public CursadasController(ICursadaRepository cursadaRepository,IUsuarioRepository usuarioRepository, IMateriaRepository materiaRepository, IMapper mapper)
        {
            _cursadaRepository = cursadaRepository;
            _usuarioRepository = usuarioRepository;
            _materiaRepository = materiaRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cursada>> GetById(int id)
        {
            var cursada = await _cursadaRepository.GetById(id);
            if (cursada == null)
            {
                return NotFound("La cursada no se ha encontrado");
            }
            
            return Ok(cursada);
        }

        [HttpGet("/api/cursadas/usuario/{usuarioId:int}")]
        public async Task<ActionResult<Cursada>> GetByUsuario(int usuarioId)
        {
            var cursadas = await _cursadaRepository.GetAllByUsuario(usuarioId);
            if (cursadas == null)
            {
                return NotFound("El usuario no existe");
            }

            return Ok(cursadas);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CursadaCreacionDTO cursadaCreacionDTO)
        {
            var usuarioExiste = await _usuarioRepository.UsuarioExists(cursadaCreacionDTO.UsuarioId);
            var materiaExiste = await _materiaRepository.MateriaExists(cursadaCreacionDTO.MateriaId);
            var cursadaExiste = await _cursadaRepository.CursadaExists(cursadaCreacionDTO.UsuarioId, cursadaCreacionDTO.MateriaId);
            if (cursadaExiste)
            {
                return BadRequest("Ya hay una cursada existente con este usuario para esta materia.");
            }
            if (materiaExiste && usuarioExiste)
            {
                var cursada = _mapper.Map<Cursada>(cursadaCreacionDTO);
                await _cursadaRepository.Insert(cursada);
                return Ok("Se cargó la inscripción a la cursada correctamente.");
            }
            return BadRequest("El usuario o la materia indicada no existe.");
            
        }

        [HttpPut("{id:int}")]
        //Solo se podra modificar la calificacion, de esta forma se aprobara o desaprobara la cursada
        public async Task<ActionResult> Update([FromBody] CursadaActualizacionDTO cursadaActualizacionDTO, int id)
        {
            var cursada = await _cursadaRepository.GetById(id);
            if (cursada == null)
            {
                return BadRequest("La cursada solicitada no existe");
            }

            _mapper.Map(cursadaActualizacionDTO, cursada);
            await _cursadaRepository.Update(cursada.CursadaId, cursada);
            return Ok("Se guardaron los datos de la cursada.");
        }


        //HttpDelete
        //Se da de baja la inscripcion

        //HttpGet
        //Se podria hacer un listado de todas las inscripciones, sin filtros
        //Listado de inscripciones de un alumno. De esta forma podemos ver a todas las materias que se inscribio un alumno, y sus notas.
        //Tambien se podria hacer un listado de inscripciones aplicando distintos filtros, pero esto se deja para mas adelante en caso de ser requerido

    }
}