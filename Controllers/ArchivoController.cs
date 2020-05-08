using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Archivo;
using Asamblea_BE.Models.Usuario;
using Asamblea_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asamblea_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArchivoController : ControllerBase
    {

        ArchvisoServices archvisoServices;
        JwtService jwtService;
        ApplicacionContext context;
      

        public ArchivoController(ApplicacionContext c)
        {
            context = c;
        }

        [HttpPost]
        [Route("uploadFile")]
        public async Task< ActionResult<string>> UploadFile(IFormFile file)
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);

                var nombreArchvio = await archvisoServices.SaveFile(file);
                return Ok(nombreArchvio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("downloadFile/{nombreArchivo}")]
        public async Task<IActionResult> downloadFile(string nombreArchivo)
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);

                var memory = await archvisoServices.Downloadfile(nombreArchivo);

                var mime = archvisoServices.GetMimeArchivoService(nombreArchivo);

                return File(memory, mime);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("New")]
        public  ActionResult<string> NuevoArchivo(NuevoArchivoModel nuevoArchivoModel)
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);
                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                nuevoArchivoModel.AgregadoPor = payload.Id;

                var guardado =  archvisoServices.NuevoArchivoService(nuevoArchivoModel);
                return Ok(guardado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("All")]
        public ActionResult<List<DocumentoDto>> Archivos()
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);
                
                var archivos = archvisoServices.Archivos();
                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("ArchivosByCategoria/{uidCategoria}")]
        public ActionResult<List<DocumentoDto>> ArchivosByCategoria(string uidCategoria)
        {
            try
            {
                if (string.IsNullOrEmpty(uidCategoria))
                    return BadRequest();

                archvisoServices = new ArchvisoServices(context);

                var archivos = archvisoServices.ArchivosByCategoriaService(uidCategoria);
                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("DeleteFile/{IdArchivo}")]
        public ActionResult<List<DocumentoDto>> DeleteFile(int IdArchivo)
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);

                var respuesta = archvisoServices.DeleteFile(IdArchivo, 1);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("NuevoRepositorio")]
        public ActionResult<string> NuevoRepositorio(NuevoRepositorioArchivoModel nuevoRepositorioArchivoModel)
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);
                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                nuevoRepositorioArchivoModel.AgregadoPor = payload.Id;
                nuevoRepositorioArchivoModel.ModificadoPor = payload.Id;

                var guardado = archvisoServices.NuevoRepositorioArchivoService(nuevoRepositorioArchivoModel);
                return Ok(guardado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("RepositoriosArchivos")]
        public ActionResult<string> RepositoriosArchivos()
        {
            try
            {
                archvisoServices = new ArchvisoServices(context);
 
                var res = archvisoServices.RepositorioArchivoService();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




    }
}
