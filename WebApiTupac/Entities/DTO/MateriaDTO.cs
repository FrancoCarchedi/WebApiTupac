using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTupac.Entities.DTO
{
    public class MateriaDTO
    {
        public int MateriaId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [ForeignKey("Carrera")]
        public int CarreraId { get; set; }
    }
}
