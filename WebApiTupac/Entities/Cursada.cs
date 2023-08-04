using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiTupac.Entities.Interfaces;

namespace WebApiTupac.Entities
{
    public class Cursada : ICursada
    {
        [Key]
        public int CursadaId { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Materia")]
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
        [Range(1, 10, ErrorMessage = "La calificación debe ser entre 1 y 10")]
        public int Calificacion { get; set; }
    }
}
