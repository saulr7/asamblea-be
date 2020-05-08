using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class EvaluacionCompletadaDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public int UsuarioId { get; set; }
        public bool Vencida { get; set; }
        public DateTime DisponibleDesde { get; set; }
        public DateTime DisponibleHasta { get; set; }
        public DateTime FechaCompletada { get; set; }

    }
}
