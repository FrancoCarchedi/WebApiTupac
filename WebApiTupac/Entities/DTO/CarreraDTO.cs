﻿using System.ComponentModel.DataAnnotations;

namespace WebApiTupac.Entities.DTO
{
    public class CarreraDTO
    {
        public int CarreraId { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public ICollection<MateriaDTO> Materias { get; set; }
    }
}
