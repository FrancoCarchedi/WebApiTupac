using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class CarreraDTO
    {
        public string CarreraId { get; set; }
        public string Nombre { get; set; }
        public string Slug {  get; set; }
        public string Descripcion {  get; set; }
        public string Titulo { get; set; }
        public string Sigla { get; set; }
        public string Portada { get; set; }
        public string Plan {  get; set; }
        public int Duracion { get; set; }
        public ICollection<MateriaDTO> Materias { get; set; }
    }
}
