using Asamblea_BE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Dtos
{
    public class EvaluacionYPreguntasDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Completada { get; set; }
        public bool Vencida { get; set; }
        public DateTime DisponibleDesde { get; set; }
        public DateTime DisponibleHasta { get; set; }
        public bool Activo { get; set; }

        public List<PreguntaDto> Preguntas { get; set; }

    }


    public class PreguntaDto
    {
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public int RespuestaSelected { get; set; }
        public List<OpcionRespuestaDto> opcionRespuestas { get; set; }

    }


    public class OpcionRespuestaDto
    {
        public int OpcionDeRespuestaId { get; set; }
        public string OpcionDeRespuesta { get; set; }

    }


}
