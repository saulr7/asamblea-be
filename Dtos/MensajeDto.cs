using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class MensajeDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Mensaje { get; set; }
        public string UsuarioUid { get; set; }
        public string ChatUid { get; set; }
        public DateTime FechaAgregado { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }

    }
}
