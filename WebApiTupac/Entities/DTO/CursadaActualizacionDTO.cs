using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class CursadaActualizacionDTO
    {
        [Range(1, 10, ErrorMessage = "La calificación debe ser entre 1 y 10")]
        public int Calificacion { get; set; }
    }
}
