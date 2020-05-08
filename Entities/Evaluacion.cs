using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Asamblea_BE.Entities
{
    [Table("Evaluaciones", Schema = "dbo")]
    public class Evaluacion
    {
        public int Id { get; set; }

        [Key]
        [Required]
        public string Uid { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime DisponibleDesde { get; set; }

        [Required]
        public DateTime DisponibleHasta { get; set; }

        [Required]
        public int AgregadoPor { get; set; }

        [Required]
        public DateTime FechaAgregado { get; set; }

        [Required]
        public DateTime FechaModificado { get; set; }

        [Required]
        public int ModificadoPor { get; set; }

        public bool Activo { get; set; }
    }
}
