namespace WebApiTupac.Entities.Interfaces
{
    public interface ICarrera
    {
        int CarreraId { get; set; }
        string Nombre { get; set; }
        int Duracion { get; set; }
        ICollection<Materia> Materias { get; set; }
    }
}
