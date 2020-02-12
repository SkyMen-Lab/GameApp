using System.Threading.Tasks;
using GameService.TCP.EventHandling;

namespace GameService.TCP
{
    public interface ITcpManager
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