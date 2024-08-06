using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTupac.Entities
{
    public class Materia
    {
        [Key]
        public Guid MateriaId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public ICollection<Cursada> Cursadas { get; set; }
        [Required]
        public Guid CarreraId { get; set; }

        [ForeignKey("CarreraId")]
        public Carrera Carrera { get; set; }
    }
}
