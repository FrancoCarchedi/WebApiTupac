namespace WebApiTupac.Entities.Interfaces
{
    public interface IMateria
    {
        int MateriaId { get; set; }
        string Nombre { get; set; }
        ICollection<Cursada> Cursadas { get; set; }
        int CarreraId { get; set; }
        Carrera Carrera { get; set; }
    }
}
