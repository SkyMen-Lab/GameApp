using System.Threading.Tasks;
using GameService.TCP.EventHandling;
using GameService.TCP.Models;

namespace GameService.TCP
{
    public interface ITcpManager
    {
        void StartServer(int port);
        void StopServer();
        Task SendPacketAsync(Packet packet);
        Task ConnectAsync(string ip, int port);
        //TODO: add game instance parameter
        Task DisconnectAsync();
    }
}