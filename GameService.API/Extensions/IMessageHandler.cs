using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.Models;

namespace GameApp.Extensions
{
    public interface IMessageHandler
    {
        Task<MessageReceived> ParseMessageAsync(ref ReadOnlySequence<byte> buffer);
        Task ProcessMessage(MessageReceived messageReceived);
        
    }
}