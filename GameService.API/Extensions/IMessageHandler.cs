using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameApp.Extensions
{
    public interface IMessageHandler
    {
        public delegate void MessageReceivedDlg(MessageReceived messageReceived);
        public event MessageReceivedDlg OnMessageReceivedEvent;

        public delegate Task MessageSentDlg<T>(MessageToSend<T> messageToSend);
        public event MessageSentDlg<KeyValuePair<string, float>> OMovementSentEvent;
        public event MessageSentDlg<Action> OnActionSentEvent;
        
        Task<MessageReceived> ParseMessageAsync(ref ReadOnlySequence<byte> buffer);
        Task SendMessageAsync<T>(MessageToSend<T> messageToSend);
        Task ProcessMessage(MessageReceived messageReceived);
    }

    public enum Action
    {
        StartGame,
        FinishGame
    }
}