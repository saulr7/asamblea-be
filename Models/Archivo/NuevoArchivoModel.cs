using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Models.Archivo
{
    public class NuevoArchivoModel
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreArchvio { get; set; }
        public string Mime { get; set; }
        public string TipoArchivo { get; set; }
        public int AgregadoPor { get; set; }
    
    }
}
