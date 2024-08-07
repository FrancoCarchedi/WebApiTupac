namespace WebApiTupac.Entities.DTO
{
    public class PerfilUsuarioDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
