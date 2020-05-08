using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class EvaluacionDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime DisponibleDesde { get; set; }
        public DateTime DisponibleHasta { get; set; }
        public bool Activo { get; set; }
    }
}
