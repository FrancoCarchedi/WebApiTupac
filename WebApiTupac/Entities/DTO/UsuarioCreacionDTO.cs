using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApiTupac.Entities.DTO
{
    public class UsuarioCreacionDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
