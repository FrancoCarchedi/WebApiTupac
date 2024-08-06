using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTupac.Entities
{
    public class Cursada
    {
        [Key]
        public Guid CursadaId { get; set; }
        [Required]
        public string UsuarioId { get; set; }
        [Required]
        public Guid MateriaId { get; set; }
        [Required]
        public string DocenteId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; }

        [ForeignKey("DocenteId")]
        public Usuario Docente { get; set; }
        [Range(1, 10, ErrorMessage = "La calificación debe ser entre 1 y 10")]
        public int Calificacion { get; set; } = 1;
        public bool Aprobada { get; set; } = false;

        public Cursada()
        {
            Calificacion = 1;
            Aprobada = false;
        }
    }
    
}
