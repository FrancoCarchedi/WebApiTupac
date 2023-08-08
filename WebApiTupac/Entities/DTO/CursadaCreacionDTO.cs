using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTupac.Entities.DTO
{
    public class CursadaCreacionDTO
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int MateriaId { get; set; }
    }
}
