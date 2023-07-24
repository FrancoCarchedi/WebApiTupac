using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class UsuarioDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Contrasena { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
