using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Asamblea_BE.Entities
{
    [Table("Mensajes", Schema = "dbo")]
    public class Mensajes
    {

        [Key]
        public int Id { get; set; }

        public string Uid { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public string UsuarioUid { get; set; }

        [Required]
        public string ChatUid { get; set; }

        [Required]
        public DateTime FechaAgregado { get; set; }



    }
}
