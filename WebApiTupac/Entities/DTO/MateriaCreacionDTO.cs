using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class MateriaCreacionDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        public string CarreraId { get; set; }
    }
}
