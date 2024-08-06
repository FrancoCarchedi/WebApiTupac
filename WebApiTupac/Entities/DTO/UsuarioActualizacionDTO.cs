using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace WebApiTupac.Entities.DTO
{
    public class UsuarioActualizacionDTO
    {
        
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        [StringLength(50)]
        public string NombreUsuario { get; set; }
        [PasswordPropertyText]
        public string Contrasena { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
