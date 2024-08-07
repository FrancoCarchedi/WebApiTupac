using System.ComponentModel.DataAnnotations;

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
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Foto { get; set; }
        [Required]
        [MinLength(1)]
        public string Roles { get; set; }
    }
}
