using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asamblea_BE.Controllers
{
    [Authorize]
    [ApiController]

    [Route("api/votacion")]

    public class VotacionController : ControllerBase
    {
        VotacionServices votacionServices;
        JwtService jwtService;
        ApplicacionContext context;


        public VotacionController(ApplicacionContext c)
        {
            context = c;
        }

        [HttpGet]
        [Route("Evaluaciones")]
        public ActionResult<List<EvaluacionDto>> Evaluaciones()
        {
            try
            {
                votacionServices = new VotacionServices(context);

                var resul = votacionServices.Evaluaciones();

                return Ok(resul);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("EvaluacionByUid/{UidEvaluacion}")]
        public ActionResult<List<EvaluacionYPreguntasDto>> EvaluacionByUid(string UidEvaluacion)
        {
            try
            {
                votacionServices = new VotacionServices(context);

                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                var usuarioId = payload.Id;

                var resul = votacionServices.EvaluacionByUid(UidEvaluacion, usuarioId);

                return Ok(resul);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarEvaluacionCompletada")]
        public ActionResult<List<EvaluacionYPreguntasDto>> GuardarEvaluacionCompletada(EvaluacionYPreguntasDto model)
        {
            try
            {
                votacionServices = new VotacionServices(context);

                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                var usuarioId = payload.Id;

                var resul = votacionServices.GuardarEvaluacionCompletada(model, usuarioId);

                return Ok(resul);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("EvaluacionesCompletadas/{uidEvaluacion}")]
        public ActionResult<List<EvaluacionCompletadaDto>> EvaluacionesCompletadas(string uidEvaluacion)
        {
            try
            {
                votacionServices = new VotacionServices(context);

                var resul = votacionServices.EvaluacionesCompletadasService(uidEvaluacion);

                return Ok(resul);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ResetearEvaluacion/{uidEvaluacion}/{idUsuario}")]
        public ActionResult<List<EvaluacionCompletadaDto>> ResetearEvaluacion(string uidEvaluacion, int idUsuario)
        {
            try
            {
                votacionServices = new VotacionServices(context);

                var resul = votacionServices.ResetearVotacionUsuario(uidEvaluacion, idUsuario);

                return Ok(resul);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
