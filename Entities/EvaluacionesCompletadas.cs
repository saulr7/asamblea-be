using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Asamblea_BE.Entities
{
    [Table("EvaluacionesCompletadas", Schema = "dbo")]
    public class EvaluacionesCompletadas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UidEvaluacion { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public bool Completada { get; set; }


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
