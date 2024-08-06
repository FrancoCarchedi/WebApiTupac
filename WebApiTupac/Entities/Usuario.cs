using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Foto { get; set; }
        //public ICollection<ApplicationRole> Roles { get; set; }
        public ICollection<Cursada> CursadasComoDocente { get; set; }
        public ICollection<Cursada> CursadasComoAlumno { get; set; }
    }
}
