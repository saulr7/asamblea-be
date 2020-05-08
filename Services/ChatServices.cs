using Asamblea_BE.Dtos;
using Asamblea_BE.Entities;
using Asamblea_BE.Models.Chat;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asamblea_BE.Services
{
    public class ChatServices
    {
        ApplicacionContext context;

        public ChatServices(ApplicacionContext c)
        {
            context = c;
        }


        public List<ChatDto> Chats()
        {
            var chats = from a in context.Chat
                        join u in context.Usuario on a.UsuarioUid equals u.Uid
                        where a.Activo
                        orderby a.FechaModificado descending
                        select new ChatDto
                        {
                            Id = a.Id,
                            Uid = a.Uid,
                            Descripcion = a.Descripcion,
                            FechaModificado = a.FechaModificado,
                            Titulo = a.Titulo,
                            UsuarioUid = a.UsuarioUid,
                            Nombre = u.Nombre,
                            Usuario = u.User
                        };
            return chats.ToList();
                
        }

        public string NuevoChat(NuevoChatModel nuevoChatModel)
        {
            if (string.IsNullOrEmpty(nuevoChatModel.Titulo) || string.IsNullOrEmpty(nuevoChatModel.Descripcion))
                throw new Exception("Datos no válidos");

            var chatUid = Guid.NewGuid().ToString();
            var nuevoChat = new Chat
            {
                Activo = true,
                AgregadoPor = nuevoChatModel.UsuarioId,
                Descripcion = nuevoChatModel.Descripcion,
                FechaAgregado = DateTime.Now,
                FechaModificado = DateTime.Now,
                ModificadoPor = nuevoChatModel.UsuarioId,
                Titulo = nuevoChatModel.Titulo,
                UsuarioUid = nuevoChatModel.UsuarioUid,
                Uid = chatUid
            };

            context.Chat.Add(nuevoChat);

            context.SaveChanges();

            return chatUid;
        }



        public List<MensajeDto> MensajesByChatUid(string uidChat)
        {
            var mensajes = from m in context.Mensajes
                           join u in context.Usuario on m.UsuarioUid equals u.Uid
                           where m.ChatUid == uidChat
                           orderby m.FechaAgregado 
                           select new MensajeDto
                           {
                               ChatUid = m.ChatUid,
                               Uid = m.Uid,
                               UsuarioUid = m.UsuarioUid,
                               FechaAgregado = m.FechaAgregado,
                               Id = m.Id,
                               Mensaje = m.Mensaje,
                               Nombre = u.Nombre,
                               Usuario = u.User
                           };

            return mensajes.ToList();
        }


        public bool NuevoMensaje(NuevoMensaje nuevoMensaje)
        {
            var mensaje = new Mensajes
            {
                FechaAgregado = DateTime.Now,
                ChatUid = nuevoMensaje.ChatUid,
                Mensaje = nuevoMensaje.Mensaje,
                UsuarioUid = nuevoMensaje.UsuarioUid,
                Uid = Guid.NewGuid().ToString()
            };

            context.Mensajes.Add(mensaje);

            context.SaveChanges();

            return true;
        }


        public async Task GetMessage(HttpContext context, WebSocket webSocket)
        {
            var messages = new[]
            {
                "Primero",
                "Segundo",
                "Tercero",
                "Cuarto"
            };

            foreach (var message in messages)
            {
                var bytes = Encoding.ASCII.GetBytes(message);
                var arraySegment = new ArraySegment<byte>(bytes);
                await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(2000); //sleeping so that we can see several messages are sent
            }

            await webSocket.SendAsync(new ArraySegment<byte>(null), WebSocketMessageType.Binary, false, CancellationToken.None);

            var buffer = new byte[4096];

            await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }


    }
}
