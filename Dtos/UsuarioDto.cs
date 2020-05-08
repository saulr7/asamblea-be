﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public bool EsAdmin { get; set; }
        public bool CambiarClave { get; set; }

    }
}
