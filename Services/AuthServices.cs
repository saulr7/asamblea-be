using Asamblea_BE.Entities;
using Asamblea_BE.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Services
{
    public class AuthServices
    {
        ApplicacionContext context;

        JwtService jwtService;

        public AuthServices(ApplicacionContext c)
        {
            context = c;
        }

        public string LoginService(CredencialesModel credenciales)
        {

            var usuario = context.Usuario.FirstOrDefault(u => u.User == credenciales.Usuario.Trim().ToLower());

            if (usuario == null)
                throw new Exception("Credenciales no válidas");


            if (!usuario.Activo)
                throw new Exception("Credenciales no válidas");

            bool validPassword = BCrypt.Net.BCrypt.Verify(credenciales.Password, usuario.Password);

            if (!validPassword)
                throw new Exception("Credenciales no válidas");


            jwtService = new JwtService();

            var payload = new PayloadModel
            {
                Usuario      = credenciales.Usuario.ToLower(),
                Id           = usuario.Id,
                Nombre       = usuario.Nombre,
                Uid          = usuario.Uid,
                CambiarClave = usuario.CambiarClave,
                EsAdmin      = usuario.EsAdmin
            };

            var token =  jwtService.GenerateSecurityToken(payload);

            return token;
        }
    }
}
