using System.Threading.Tasks;
using GameApp.Extensions;

namespace GameService.TCP
{
    public interface ITcpManager
    {
        bool IsConnected { get; }
        Task SendMessageAsync(string message);
        Task ConnectAsync(string ip, int port);
        //TODO: add game instance parameter
        Task DisconnectAsync();
    }
}