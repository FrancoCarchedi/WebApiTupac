using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class CarreraCreacionDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Portada { get; set; }
        [Required]
        [StringLength(20)]
        public string Sigla { get; set; }
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        public string Plan { get; set; }
        [Required]
        [Range(1, 6)]
        public int Duracion { get; set; }
    }
}
