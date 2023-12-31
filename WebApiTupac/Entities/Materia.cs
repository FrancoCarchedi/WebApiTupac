﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiTupac.Entities.Interfaces;

namespace WebApiTupac.Entities
{
    public class Materia : IMateria
    {
        [Key]
        public int MateriaId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public ICollection<Cursada> Cursadas { get; set; }
        [Required]
        [ForeignKey("Carrera")]
        public int CarreraId { get; set; }
        //Esto se conoce como propiedad de navegacion
        public Carrera Carrera { get; set; }
    }
}
