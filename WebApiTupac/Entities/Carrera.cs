﻿using System.ComponentModel.DataAnnotations;
using WebApiTupac.Entities.Interfaces;

namespace WebApiTupac.Entities
{
    public class Carrera : ICarrera
    {
        [Key]
        public int CarreraId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Range(1, 6)]
        public int Duracion { get; set; }
        public ICollection<Materia> Materias { get; set; }
    }
}
