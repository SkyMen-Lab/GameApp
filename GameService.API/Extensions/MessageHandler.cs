using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.Models;

namespace GameApp.Extensions
{
    public class MessageHandler : IMessageHandler
    {

        public Task<MessageReceived> ParseMessageAsync(ref ReadOnlySequence<byte> buffer)
        {
            return null;
        }

        public Task SendMessageAsync<T>(MessageToSend<T> messageToSend)
        {
            return null;
        }

        public Task ProcessMessage(MessageReceived messageReceived)
        {
            return null;
        }
    }
}