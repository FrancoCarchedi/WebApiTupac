using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class UsuariosDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [EmailAddress]
        public string Email { get; set; }

    }
}
