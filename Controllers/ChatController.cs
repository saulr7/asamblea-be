using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Chat;
using Asamblea_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asamblea_BE.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        ChatServices chatServices;
        JwtService jwtService;
        ApplicacionContext context;

        public ChatController(ApplicacionContext c)
        {
            context = c;
        }

        [HttpGet]
        [Route("ws")]
        [AllowAnonymous]
        public async Task Get()
        {
            var contextSocket = ControllerContext.HttpContext;
            var isSocketRequest = contextSocket.WebSockets.IsWebSocketRequest;

            //if(isSocketRequest)
            //{
                WebSocket webSocket = await contextSocket.WebSockets.AcceptWebSocketAsync("protocolOne");

                chatServices = new ChatServices(context);

                await chatServices.GetMessage(contextSocket, webSocket);

            //}
            //else
            //{
            //    contextSocket.Response.StatusCode = 400;
            //}
        }




        [HttpGet]
        [Route("chats")]
        public ActionResult<List<ChatDto>> Chats()
        {
            try
            {
                chatServices = new ChatServices(context);

                var chats = chatServices.Chats();

                return Ok(chats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("NuevoChat")]
        public ActionResult<string> NuevoChat(NuevoChatModel nuevoChatModel)
        {
            try
            {
                chatServices = new ChatServices(context);

                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                nuevoChatModel.UsuarioId = payload.Id;
                nuevoChatModel.UsuarioUid = payload.Uid;

                var chats = chatServices.NuevoChat(nuevoChatModel);

                return Ok(chats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("MensajesByChatUid/{chatUid}")]
        public ActionResult<bool> MensajesByChatUid(string chatUid)
        {
            try
            {
                chatServices = new ChatServices(context);

                var mensajes = chatServices.MensajesByChatUid(chatUid);

                return Ok(mensajes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("NuevoMensaje")]
        public ActionResult<bool> NuevoMensaje(NuevoMensaje nuevoMensaje)
        {
            try
            {
                chatServices = new ChatServices(context);

                jwtService = new JwtService();

                var payload = jwtService.GetPayload(Request);

                nuevoMensaje.UsuarioId = payload.Id;
                nuevoMensaje.UsuarioUid = payload.Uid;

                var enviado = chatServices.NuevoMensaje(nuevoMensaje);

                return Ok(enviado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
