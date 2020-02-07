using System.Threading.Tasks;
using GameApp.Extensions;
using GameService.TCP.EventHandling;

namespace GameService.TCP
{
    public interface ITcpManager : IEventDisposer
    {
        void StartServer(int port);
        void StopServer();
        bool IsConnected { get; }
        Task SendMessageAsync(string message);
        Task ConnectAsync(string ip, int port);
        //TODO: add game instance parameter
        Task DisconnectAsync();
    }
}