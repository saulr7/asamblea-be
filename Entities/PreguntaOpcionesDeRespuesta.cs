using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asamblea_BE.Entities
{
    [Table("PreguntaOpcionesDeRespuesta", Schema = "dbo")]
    public class PreguntaOpcionesDeRespuesta
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int PreguntaId { get; set; }

        [Required]
        public int OpcionDeRespuestaId { get; set; }

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
