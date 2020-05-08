using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Archivo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asamblea_BE.Services
{
    public class ArchvisoServices
    {
        public ArchvisoServices()
        {

        }

        ApplicacionContext context;

        public ArchvisoServices(ApplicacionContext c)
        {
            context = c;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file == null)
                throw new Exception("No se ha proporcionado el archvio");

            EscapeFileName(file.FileName);

            var today = DateTime.Now.ToString("yyyyMMddHHmmss");
            var nombreArchivo = today + file.FileName;

            var UrlArchivos = context.VariableDeConfiguracion.Where(v => v.Variable == "UlrArchivos").FirstOrDefault().Valor.ToString();

            var path = Path.Combine(UrlArchivos, "wwwroot", nombreArchivo );

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return nombreArchivo;
        }

        public async Task<MemoryStream> Downloadfile(string fileName)
        {

            var UrlArchivos = context.VariableDeConfiguracion.Where(v => v.Variable == "UlrArchivos").FirstOrDefault().Valor.ToString();

            var path = Path.Combine(UrlArchivos, "wwwroot", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return memory;
            
        }


        public bool NuevoArchivoService(NuevoArchivoModel nuevoArchivoModel)
        {

            var nuevoArchvio = new Archivo
            {
                Activo              = true,
                AgregadoPor         = nuevoArchivoModel.AgregadoPor,
                FechaAgregado       = DateTime.Now,
                FechaModificado     = DateTime.Now,
                ModificadoPor       = nuevoArchivoModel.AgregadoPor,
                NombreArchivo       = nuevoArchivoModel.NombreArchvio,
                TipoArchivo         = nuevoArchivoModel.TipoArchivo,
                Titulo              = nuevoArchivoModel.Titulo,
                Descripcion         = nuevoArchivoModel.Descripcion,
                Mime                = nuevoArchivoModel.Mime,


            };

            context.Archivo.Add(nuevoArchvio);

            var saved = context.SaveChanges();
           
            return true;

        }


        public string GetMimeArchivoService(string nombreArchivo)
        {

            var mime = context.Archivo.FirstOrDefault(a => a.NombreArchivo == nombreArchivo).Mime.ToString();

            return mime;

        }



        public List<DocumentoDto> Archivos()
        {

            var archivosList = context.Archivo.Where(a => a.Activo).OrderBy(b => b.Orden).ThenBy(b => b.Id).ToList();

            var archivos = from a in archivosList
                           select new DocumentoDto
                           {
                               Descripcion    = a.Descripcion,
                               Mime           = a.Mime,
                               NombreArchivo  = a.NombreArchivo,
                               TipoArchivo    = a.TipoArchivo,
                               Titulo         = a.Titulo,
                               Id             = a.Id

                           };


            return archivos.ToList();

        }

        public List<DocumentoDto> ArchivosByCategoriaService(string ArchivosByCategoria)
        {

            var archivosList = context.Archivo.Where(a => a.Activo && a.UidRepositorioArchivo == ArchivosByCategoria).OrderBy(b => b.Orden).ThenBy(b => b.Id).ToList();

            var archivos = from a in archivosList
                           select new DocumentoDto
                           {
                               Descripcion = a.Descripcion,
                               Mime = a.Mime,
                               NombreArchivo = a.NombreArchivo,
                               TipoArchivo = a.TipoArchivo,
                               Titulo = a.Titulo,
                               Id = a.Id

                           };


            return archivos.ToList();

        }


        public int DeleteFile(int IdArchivo, int IdUsuario)
        {

            var archivo = context.Archivo.Where(a => a.Id == IdArchivo).FirstOrDefault();

            archivo.Activo = false;
            archivo.FechaModificado = DateTime.Now;
            archivo.ModificadoPor = IdUsuario;

            return context.SaveChanges();

        }

        public bool NuevoRepositorioArchivoService(NuevoRepositorioArchivoModel nuevoRepositorioArchivoModel)
        {
            if (string.IsNullOrEmpty(nuevoRepositorioArchivoModel.Titulo) || string.IsNullOrEmpty(nuevoRepositorioArchivoModel.Descripcion))
                throw new Exception("Datos incorrectos");

            var nuevoArchvio = new RepositorioArchivo
            {
                Activo = true,
                AgregadoPor = nuevoRepositorioArchivoModel.AgregadoPor,
                FechaAgregado = DateTime.Now,
                FechaModificado = DateTime.Now,
                ModificadoPor = nuevoRepositorioArchivoModel.AgregadoPor,
                Titulo = nuevoRepositorioArchivoModel.Titulo,
                Descripcion = nuevoRepositorioArchivoModel.Descripcion,
                Uid         =  Guid.NewGuid().ToString()

            };

            context.RepositorioArchivo.Add(nuevoArchvio);

            context.SaveChanges();

            return true;

        }



        public int DeleteCategoriaService(int IdArchivo, int IdUsuario)
        {

            var archivo = context.Archivo.Where(a => a.Id == IdArchivo).FirstOrDefault();

            archivo.Activo = false;
            archivo.FechaModificado = DateTime.Now;
            archivo.ModificadoPor = IdUsuario;

            return context.SaveChanges();

        }


        public List<RepositorioArchivo> RepositorioArchivoService()
        {
            var repositorios =   context.RepositorioArchivo.Where(r => r.Activo).OrderByDescending(r => r.Id).ToList();

            return repositorios;

        }



        private void EscapeFileName(string fileName)
        {
            string extention = fileName.Split(".").Last().ToString();

            if (string.IsNullOrEmpty(extention))
                throw new Exception("Archivo no válido");
        }

    }
}
