using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Models.Auth
{
    public class CambiarPasswordModel
    {
        public string Uid { get; set; }
        public string PasswordActual { get; set; }
        public string NuevoPassword { get; set; }
        public string ConfirmacionPassword { get; set; }

    }
}
