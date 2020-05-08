using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Asamblea_BE.Entities
{
    [Table("Chats", Schema = "dbo")]
    public class Chat
    {

        [Key]
        public int Id { get; set; }

        public string Uid { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string UsuarioUid { get; set; }


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
