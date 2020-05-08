using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Auth;
using Asamblea_BE.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Services
{
    public class UsuarioServices
    {
        ApplicacionContext context;

        public UsuarioServices(ApplicacionContext c)
        {
            context = c;
        }


        public List<UsuarioDto> Usuarios()
        {
           
            var usuariosList = context.Usuario.ToList();

            var usuarios = from u in usuariosList
                          select new UsuarioDto
                          {
                              Activo = u.Activo,
                              CambiarClave = u.CambiarClave,
                              EsAdmin = u.EsAdmin,
                              Id = u.Id,
                              Nombre = u.Nombre,
                              Uid = u.Uid,
                              Usuario = u.User
                          };
           
            return usuarios.ToList();

        }

        public string NuevoUsuarioService(NuevoUsuarioModel nuevoUsuarioModel)
        {
            var newPassword = RandomString(8);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            var nuevoUsuario = new Usuario
            {
                Activo = true,
                AgregadoPor = nuevoUsuarioModel.AgregadoPor,
                CambiarClave = true,
                EsAdmin = false,
                FechaAgregado = DateTime.Now,
                FechaModificado = DateTime.Now,
                UltimoAcceso    = DateTime.Now,
                ModificadoPor = nuevoUsuarioModel.ModificadoPor,
                Nombre = nuevoUsuarioModel.Nombre,
                Password = hashedPassword,
                User = nuevoUsuarioModel.Usuario.Trim().ToLower(),
                Token = "",
                Uid = Guid.NewGuid().ToString(),


            };

            context.Usuario.Add(nuevoUsuario);
            var r = context.SaveChanges();

            return newPassword;

        }

        public bool CambiarPasswordService(CambiarPasswordModel cambiarPasswordModel)
        {
            var usuario = context.Usuario.FirstOrDefault(u => u.Uid == cambiarPasswordModel.Uid);

            if (usuario == null)
                throw new Exception("Credenciales no válidas");


            bool validPassword = BCrypt.Net.BCrypt.Verify(cambiarPasswordModel.PasswordActual, usuario.Password);

            if (!validPassword)
                throw new Exception("Credenciales no válidas");

            validateNewPassword(cambiarPasswordModel);


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(cambiarPasswordModel.NuevoPassword);

            usuario.Password = hashedPassword;
            usuario.CambiarClave = false;
            usuario.FechaModificado = DateTime.Now;


            context.SaveChanges();

            return true;

        }

        public string ResetearPasswordService(ResetearPasswordModel resetearPasswordModel)
        {
            var usuario = context.Usuario.FirstOrDefault(u => u.Uid == resetearPasswordModel.Uid);

            if (usuario == null)
                throw new Exception("No se ha encontrado el usuario");

            var newPassword = RandomString(8);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);


            usuario.Password = hashedPassword;
            usuario.CambiarClave = true;
            usuario.FechaModificado = DateTime.Now;
            usuario.ModificadoPor = resetearPasswordModel.ModificadoPor;


            context.SaveChanges();

            return newPassword;

        }


        public string VaribaleDeConfiguracion(string variable)
        {
            var _variable = context.VariableDeConfiguracion.Where(v => v.Variable == variable).FirstOrDefault().Valor.ToString();

            return _variable;

        }


        private void validateNewPassword(CambiarPasswordModel cambiarPasswordModel)
        {
            if (string.IsNullOrEmpty(cambiarPasswordModel.NuevoPassword))
                throw new Exception("Nueva contraseña no válida");

            if (cambiarPasswordModel.NuevoPassword.Length <6)
                throw new Exception("La nueva contraseña debe contener al menos 6 carácteres");

            if (cambiarPasswordModel.NuevoPassword == cambiarPasswordModel.PasswordActual)
                throw new Exception("La nueva contraseña no puede ser igual a la contraseña actual");


            if (cambiarPasswordModel.NuevoPassword != cambiarPasswordModel.ConfirmacionPassword)
                throw new Exception("La nueva contraseña no coincide");
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
