using Asamblea_BE.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Asamblea_BE.Services
{
    public class JwtService
    {
        private readonly string _secret = "my-s3cr3t#@-ha-ha!";
        private readonly string _expDate ="240";

        public JwtService()
        {

        }

        public string GenerateSecurityToken(PayloadModel payload)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Usuario", payload.Usuario),
                    new Claim("Uid", payload.Uid),
                    new Claim("Nombre", payload.Nombre),
                    new Claim("Id", payload.Id.ToString()),
                    new Claim("CambiarClave", payload.CambiarClave.ToString()),
                    new Claim("EsAdmin", payload.EsAdmin.ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


        public PayloadModel GetPayload(HttpRequest request)
        {

            var token = "";
            var headers = request.Headers;

            if (headers.ContainsKey("Authorization"))
            {
                token = headers["Authorization"];
                token = token.StartsWith("Bearer ") ? token.Substring(7) : token;
            }

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            PayloadModel payload = new PayloadModel();

            var claims = tokenS.Claims;
            foreach (Claim c in claims)
            {
                if (c.Type == "Usuario")
                {
                    payload.Usuario = c.Value;
                }

                if (c.Type == "Uid")
                    payload.Uid = c.Value;

                if (c.Type == "EsAdmin")
                    payload.EsAdmin = Convert.ToBoolean( c.Value);

                if (c.Type == "Id")
                    payload.Id = Convert.ToInt32(c.Value);

            }
            return payload;
        }
    }
}
