using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities
{
    public class Carrera
    {
        [Key]
        public Guid CarreraId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Portada { get; set; }
        public string Sigla { get; set; }
        public string Titulo { get; set; }
        public string Plan {  get; set; }
        [Required]
        [Range(1, 6)]
        public int Duracion { get; set; }
        public ICollection<Materia> Materias { get; set; }
        public string Slug { get; set; }
        public bool Estado { get; set; } = true;

        public Carrera()
        {
            Estado = true; // Valor por defecto para Estado
        }
    }
}
