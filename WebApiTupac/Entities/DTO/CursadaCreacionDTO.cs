using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTupac.Entities.DTO
{
    public class CursadaCreacionDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid MateriaId { get; set; }
        public string DocenteId { get; set; }
    }
}
