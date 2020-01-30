using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameApp.Extensions
{
    public class MessageHandler : IMessageHandler
    {
        public event IMessageHandler.MessageReceivedDlg OnMessageReceivedEvent;
        public event IMessageHandler.MessageSentDlg<KeyValuePair<string, float>> OMovementSentEvent;
        public event IMessageHandler.MessageSentDlg<Action> OnActionSentEvent;
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