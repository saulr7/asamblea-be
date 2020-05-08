using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Auth;
using Asamblea_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asamblea_BE.Controllers
{

    [Authorize]
    [ApiController]

    [Route("api/auth")]

    public class AuthController : ControllerBase
    {
        ApplicacionContext context;
        AuthServices authServices;

        public AuthController(ApplicacionContext c)
        {
            context = c;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] CredencialesModel credenciales)
        {
            try
            {
                
                authServices = new AuthServices(context);
                var result = authServices.LoginService(credenciales);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
