namespace WebApiTupac.Entities.Interfaces
{
    public interface IUsuario
    {
        int UsuarioId { get; set; }
        string Nombre { get; set; }
        string Apellido { get; set; }
        DateTime FechaNacimiento { get; set; }
        string NombreUsuario { get; set; }
        string Contrasena { get; set; }
        string Email { get; set; }
    }
}
