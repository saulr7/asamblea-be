using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Auth;
using Asamblea_BE.Models.Usuario;
using Asamblea_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asamblea_BE.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/usuario")]

    public class UsuarioController : ControllerBase
    {
        ApplicacionContext context;
        UsuarioServices usuarioServices;
        JwtService jwtService;

        public UsuarioController(ApplicacionContext c)
        {
            context = c;
        }

        [HttpPost]
        [Route("New")]
        public ActionResult< string> NuevoUsuario([FromBody] NuevoUsuarioModel nuevoUsuarioModel)
        {
            try
            {
                jwtService = new JwtService();
                usuarioServices = new UsuarioServices(context);

                var payload = jwtService.GetPayload(Request);
                nuevoUsuarioModel.AgregadoPor = payload.Id;
                nuevoUsuarioModel.ModificadoPor = payload.Id;
                var result = usuarioServices.NuevoUsuarioService(nuevoUsuarioModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("CambiarPassword")]
        public ActionResult<string> CambiarPassword([FromBody] CambiarPasswordModel cambiarPasswordModel)
        {
            try
            {
                usuarioServices = new UsuarioServices(context);

                var result = usuarioServices.CambiarPasswordService(cambiarPasswordModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("usuarios")]
        public ActionResult<List<UsuarioDto>> Usuarios()
        {
            try
            {
                usuarioServices = new UsuarioServices(context);

                var usuarios = usuarioServices.Usuarios();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        [Route("ResetearPassword")]
        public ActionResult<string> ResetearPassword(ResetearPasswordModel resetearPasswordModel)
        {
            try
            {
                usuarioServices = new UsuarioServices(context);
                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                if (payload.Uid == resetearPasswordModel.Uid)
                    return BadRequest("Acción no permitida");

                resetearPasswordModel.ModificadoPor = payload.Id;
                var newPassword = usuarioServices.ResetearPasswordService(resetearPasswordModel);

                return Ok(newPassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("VariableConfiguracion/{variable}")]
        public ActionResult<string> VariableConfiguracion(string variable)
        {
            try
            {
                usuarioServices = new UsuarioServices(context);
               
                var respuesta = usuarioServices.VaribaleDeConfiguracion(variable);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
