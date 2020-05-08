using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Models.Archivo
{
    public class NuevoRepositorioArchivoModel
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int ModificadoPor { get; set; }
        public int AgregadoPor { get; set; }
    }
}
