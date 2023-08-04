using Microsoft.AspNetCore.Mvc;

namespace WebApiTupac.Controllers
{
    [Route("api/usuarios/{UsuarioId:int}/cursadas")]
    [ApiController]
    public class CursadasController : ControllerBase
    {
        public CursadasController()
        {

        }

        //[HttpPost]
        //Alta de inscripcion
        //Solo se le pasara el alumno y la materia

        //HttpPut
        //Update de la inscripcion
        //Solo se podra modificar la calificacion, de esta forma se aprobara o desaprobara la cursada

        //HttpDelete
        //Se da de baja la inscripcion

        //HttpGet
        //Se podria hacer un listado de todas las inscripciones, sin filtros
        //Listado de inscripciones de un alumno. De esta forma podemos ver a todas las materias que se inscribio un alumno, y sus notas.
        //Tambien se podria hacer un listado de inscripciones aplicando distintos filtros, pero esto se deja para mas adelante en caso de ser requerido

    }
}
