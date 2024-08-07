using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class CarreraActualizacionDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Portada { get; set; }
        public string Sigla { get; set; }
        public string Titulo { get; set; }
        public string Plan { get; set; }
        [Range(1, 6)]
        public int Duracion { get; set; }
    }
}
