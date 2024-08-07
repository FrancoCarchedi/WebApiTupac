using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class UsuarioActualizacionDTO
    {
        
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
