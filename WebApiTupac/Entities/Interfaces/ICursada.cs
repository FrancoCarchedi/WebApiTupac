namespace WebApiTupac.Entities.Interfaces
{
    public interface ICursada
    {
        int CursadaId { get; set; }
        int UsuarioId { get; set; }
        Usuario Usuario { get; set; }
        int MateriaId { get; set; }
        Materia Materia { get; set; }
        int Calificacion { get; set; }
    }
}
