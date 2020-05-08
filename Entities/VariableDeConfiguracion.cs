using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Asamblea_BE.Entities
{

    [Table("VariablesDeConfiguracion", Schema = "dbo")]
    public class VariableDeConfiguracion
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Variable { get; set; }

        [Required]
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
