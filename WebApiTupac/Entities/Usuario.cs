using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApiTupac.Entities.Interfaces;

namespace WebApiTupac.Entities
{
    public class Usuario : IUsuario
    {
        [Key]
        [Required]
        public int UsuarioId { get; set; }
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
