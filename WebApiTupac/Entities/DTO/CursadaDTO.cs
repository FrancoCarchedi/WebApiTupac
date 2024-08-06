

namespace WebApiTupac.Entities.DTO
{
    public class CursadaDTO
    {
        public Guid CursadaId { get; set; }
        public string UsuarioId { get; set; }
        public Guid MateriaId { get; set; }
        public string DocenteId { get; set; }
        public Usuario Usuario { get; set; }
        public Materia Materia { get; set; }
        public Usuario Docente { get; set; }
        public int Calificacion { get; set; }
        public bool Aprobada { get; set; }
    }
}
