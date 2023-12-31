﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApiTupac.Entities.DTO
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        [PasswordPropertyText]
        public string Contrasena { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Cursada> Cursadas { get; set; }
    }
}
