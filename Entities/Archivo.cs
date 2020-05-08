using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Asamblea_BE.Entities
{
    [Table("Archivos", Schema = "dbo")]
    public class Archivo
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string NombreArchivo { get; set; }

        public string Mime { get; set; }

        public string TipoArchivo { get; set; }


        [Required]
        public string UidRepositorioArchivo { get; set; }

        public int Orden { get; set; }

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
