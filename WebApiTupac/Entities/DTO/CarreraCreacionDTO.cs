using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class CarreraCreacionDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Range(1, 6)]
        public int Duracion { get; set; }
    }
}
