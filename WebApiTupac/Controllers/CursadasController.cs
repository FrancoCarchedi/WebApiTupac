using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public CursadasController(ICursadaRepository cursadaRepository, IUsuarioRepository usuarioRepository, IMateriaRepository materiaRepository, IMapper mapper)
        {
            _cursadaRepository = cursadaRepository;
            _usuarioRepository = usuarioRepository;
            _materiaRepository = materiaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CursadaDTO>>> Get()
        {
            var cursadas = await _cursadaRepository.GetAll();
            var cursadasDTO = _mapper.Map<IEnumerable<CursadaDTO>>(cursadas);
            return Ok(cursadasDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cursada>> GetById(string id)
        {
            var cursada = await _cursadaRepository.GetById(id);
            if (cursada == null)
            {
                return NotFound("La cursada no se ha encontrado");
            }
            
            return Ok(cursada);
        }

        [HttpGet("/api/cursadas/usuario/{usuarioId}")]
        public async Task<ActionResult<Cursada>> GetByUsuario(string usuarioId)
        {
            var cursadas = await _cursadaRepository.GetAllByUsuario(usuarioId);
            if (cursadas == null)
            {
                return NotFound("El usuario no existe");
            }

            return Ok(cursadas);
        }

        [HttpGet("/api/cursadas/docente/{docenteId}")]
        public async Task<ActionResult<Cursada>> GetByDocente(string docenteId)
        {
            var cursadas = await _cursadaRepository.GetAllByDocente(docenteId);
            if (cursadas == null)
            {
                return NotFound("El usuario no existe");
            }

            return Ok(cursadas);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromForm] CursadaCreacionDTO cursadaCreacionDTO)
        {
            // Buscar por el mail
            var alumnoExiste = await _usuarioRepository.GetByMail(cursadaCreacionDTO.Email);
            var docenteExiste = await _usuarioRepository.GetById(cursadaCreacionDTO.DocenteId);
            var materiaExiste = await _materiaRepository.GetById(cursadaCreacionDTO.MateriaId.ToString());

            var cursadaExiste = await _cursadaRepository.CursadaExists(alumnoExiste.Id, materiaExiste.MateriaId.ToString());
            if (cursadaExiste)
            {
                //return BadRequest("Ya hay una cursada existente con este usuario para esta materia.");
                return Ok("Se cargó la inscripción a la cursada correctamente.");
            }

            if ((materiaExiste != null) && (alumnoExiste != null) && (docenteExiste != null))
            {
                var cursada = _mapper.Map<Cursada>(cursadaCreacionDTO);
                cursada.UsuarioId = alumnoExiste.Id;

                await _cursadaRepository.Insert(cursada);
                return Ok("Se cargó la inscripción a la cursada correctamente.");
            }
            return BadRequest("El usuario o la materia indicada no existe.");
            
        }

        [HttpPatch("{id}")]
        //Solo se podra modificar la calificacion, de esta forma se aprobara o desaprobara la cursada
        //[Authorize(Roles = "Docente,Administrador")]
        public async Task<ActionResult> Update([FromForm] CursadaActualizacionDTO cursadaActualizacionDTO, string id)
        {
            var cursada = await _cursadaRepository.GetById(id);
            if (cursada is null)
            {
                return BadRequest("La cursada solicitada no existe");
            }

            cursada.Aprobada = cursadaActualizacionDTO.Calificacion >= 4;

            _mapper.Map(cursadaActualizacionDTO, cursada);
            await _cursadaRepository.Update(cursada.CursadaId.ToString(), cursada);
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