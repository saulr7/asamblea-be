using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioUid { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public DateTime  FechaModificado { get; set; }


    }
}
