using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Models.Chat
{
    public class NuevoChatModel
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioUid { get; set; }
        public int UsuarioId { get; set; }
    }
}
