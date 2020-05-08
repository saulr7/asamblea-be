using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Services
{
    public class VotacionServices
    {

        ApplicacionContext context;

        public VotacionServices(ApplicacionContext c)
        {
            context = c;
        }


        public List<EvaluacionDto> Evaluaciones()
        {
            var evaluacionesList = context.Evaluacion.Where(e => e.Activo).ToList();

            var evaluaciones = from e in evaluacionesList
                               select new EvaluacionDto
                               {
                                   Activo = e.Activo,
                                   Descripcion = e.Descripcion,
                                   Titulo = e.Titulo,
                                   DisponibleDesde = e.DisponibleDesde,
                                   DisponibleHasta = e.DisponibleHasta,
                                   Uid = e.Uid,
                                   Id = e.Id
                               };

            return evaluaciones.ToList();
        }




        public EvaluacionYPreguntasDto EvaluacionByUid(string UidEvaluacion, int usuarioId)
        {
            EvaluacionYPreguntasDto evaluacionDto;

            var evaluacion = context.Evaluacion.FirstOrDefault(e => e.Uid == UidEvaluacion);

            var completada = false;
            var evaluacionCompletada = context.EvaluacionesCompletadas.FirstOrDefault(v => v.UidEvaluacion == UidEvaluacion && v.IdUsuario == usuarioId);

            if (evaluacionCompletada != null && evaluacionCompletada.Completada == true)
                completada = true;

            evaluacionDto = new EvaluacionYPreguntasDto
            {
                Id = evaluacion.Id,
                Uid = evaluacion.Uid,
                Activo = evaluacion.Activo,
                Descripcion = evaluacion.Descripcion,
                DisponibleDesde = evaluacion.DisponibleDesde,
                DisponibleHasta = evaluacion.DisponibleHasta,
                Titulo = evaluacion.Titulo,
                Completada = completada,
                Vencida = (evaluacion.DisponibleHasta > DateTime.Now && evaluacion.DisponibleDesde < DateTime.Now ? false : true),
                Preguntas = null

            };

            var preguntas = (from p in context.Pregunta
                             where p.UidEvaluacion == UidEvaluacion
                             select new PreguntaDto
                             {
                                 Id = p.Id,
                                 Pregunta = p.Pregunta,
                                 opcionRespuestas = (from a in context.PreguntaOpcionesDeRespuestas
                                                    join b in context.OpcionesDeRespuestas on a.OpcionDeRespuestaId equals b.Id 
                                                    where a.PreguntaId == p.Id
                                                    select new OpcionRespuestaDto
                                                    {
                                                        OpcionDeRespuesta   = b.OpcionDeRespuesta,
                                                        OpcionDeRespuestaId = b.Id
                                                    }
                                                     ).ToList(),
                                 RespuestaSelected = (from r in context.Respuesta
                                                     where r.UidEvaluacion == UidEvaluacion && r.UsuarioId == usuarioId && r.PreguntaId 
                                                     == p.Id
                                                     select r.OpcionDeRespuestaId).DefaultIfEmpty().First()
                             }
                             );

            evaluacionDto.Preguntas = preguntas.ToList();


            return evaluacionDto;
        }


        public List<EvaluacionCompletadaDto> EvaluacionesCompletadasService(string UidEvaluacion)
        {
            var evaluaciones = from ec in context.EvaluacionesCompletadas
                               join e in context.Evaluacion on ec.UidEvaluacion equals e.Uid
                               join u in context.Usuario on ec.IdUsuario equals u.Id
                               where ec.UidEvaluacion == UidEvaluacion
                               select new EvaluacionCompletadaDto
                               {
                                   Id = ec.Id,
                                   DisponibleDesde = e.DisponibleDesde,
                                   DisponibleHasta = e.DisponibleHasta,
                                   FechaCompletada = ec.FechaAgregado,
                                   Nombre = u.Nombre,
                                   Uid = ec.UidEvaluacion,
                                   Usuario = u.User,
                                   Vencida = (e.DisponibleHasta > DateTime.Now && e.DisponibleDesde < DateTime.Now ? false : true),
                                   UsuarioId = u.Id

                               };

            return evaluaciones.ToList();
        }


        public bool GuardarEvaluacionCompletada(EvaluacionYPreguntasDto evaluacion, int usuarioId)
        {
            validateEvaluacion(evaluacion, usuarioId);

            using var transaction = context.Database.BeginTransaction();

            try
            {

                foreach (var pregunta in evaluacion.Preguntas)
                {
                    var respuesta = new Respuesta
                    {
                        Activo = true,
                        AgregadoPor = usuarioId,
                        ModificadoPor = usuarioId,
                        UsuarioId = usuarioId,
                        FechaAgregado = DateTime.Now,
                        FechaModificado = DateTime.Now,
                        Valor = "",
                        UidEvaluacion = evaluacion.Uid,
                        PreguntaId = pregunta.Id,
                        OpcionDeRespuestaId = pregunta.RespuestaSelected

                    };

                    context.Add(respuesta);

                }

                var evaluacionCompleta = new EvaluacionesCompletadas
                {
                    Activo = true,
                    AgregadoPor = usuarioId,
                    ModificadoPor = usuarioId,
                    Completada = true,
                    FechaAgregado = DateTime.Now,
                    FechaModificado = DateTime.Now,
                    IdUsuario = usuarioId,
                    UidEvaluacion = evaluacion.Uid
                };

                context.Add(evaluacionCompleta);

                context.SaveChanges();
                transaction.Commit();

            return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }


        public bool ResetearVotacionUsuario(string UidEvaluacion, int usuarioId)
        {

            using var transaction = context.Database.BeginTransaction();
            try
            {

                var evaluacionCompletada = context.EvaluacionesCompletadas.FirstOrDefault(e => e.UidEvaluacion == UidEvaluacion && e.IdUsuario == usuarioId);

                if (evaluacionCompletada == null)
                    throw new Exception("No se ha encontrado la evaluación");

                context.Remove(evaluacionCompletada);

                var respuestas = context.Respuesta.Where(x => x.UidEvaluacion == UidEvaluacion && x.UsuarioId == usuarioId);

                context.RemoveRange(respuestas.ToList());

                context.SaveChanges();

                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }


        private void validateEvaluacion(EvaluacionYPreguntasDto evaluacion, int usuarioId)
        {
            if (string.IsNullOrEmpty(evaluacion.Uid))
                throw new Exception("Datos incorrectos");

            var evaluacionCompletada = context.EvaluacionesCompletadas.FirstOrDefault(e => e.UidEvaluacion == evaluacion.Uid && e.IdUsuario == usuarioId);

            if (evaluacionCompletada != null)
                throw new Exception("La votación ya ha sido realizada previamente");

            foreach (var pregunta in evaluacion.Preguntas)
            {
                if(pregunta.Id <= 0 || pregunta.RespuestaSelected <= 0)
                    throw new Exception("Preguntas y/o respuestas no válidas");


            }


        }

    }
}

