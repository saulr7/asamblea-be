using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Asamblea_BE.Entities
{
    [Table("Usuarios", Schema = "dbo")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Uid { get; set; }

        [Required]
 
        public string User { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int AgregadoPor { get; set; }

        [Required]
        public DateTime FechaAgregado { get; set; }

        [Required]
        public DateTime FechaModificado { get; set; }

        [Required]
        public int ModificadoPor { get; set; }

        [Required]
        public bool CambiarClave { get; set; }

        
        public DateTime UltimoAcceso { get; set; }

        public bool Activo { get; set; }

        public bool EsAdmin { get; set; }

        public string Token { get; set; }

    }
}
