using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Models.Chat
{
    public class NuevoMensaje
    {
        public string Mensaje { get; set; }
        public string ChatUid { get; set; }
        public string UsuarioUid { get; set; }
        public int UsuarioId { get; set; }
    }
}
