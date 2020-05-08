using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Models.Usuario
{
    public class NuevoUsuarioModel
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public int AgregadoPor { get; set; }
        public int ModificadoPor { get; set; }

    }
}
