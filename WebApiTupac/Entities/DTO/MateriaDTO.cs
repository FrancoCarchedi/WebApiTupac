using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTupac.Entities.DTO
{
    public class MateriaDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [ForeignKey("Carrera")]
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
        public ICollection<Cursada> Cursadas { get; }
    }
}
