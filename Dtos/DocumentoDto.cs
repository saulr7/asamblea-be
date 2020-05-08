using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class DocumentoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreArchivo { get; set; }
        public string Mime { get; set; }
        public string TipoArchivo { get; set; }

    }
}
