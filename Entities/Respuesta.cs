using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Asamblea_BE.Entities
{
    [Table("Respuestas", Schema = "dbo")]
    public class Respuesta
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string UidEvaluacion { get; set; }

        [Required]
        public int PreguntaId { get; set; }

        [Required]
        public int OpcionDeRespuestaId { get; set; }

        public string Valor { get; set; }

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
