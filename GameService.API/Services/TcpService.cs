using System.Threading;
using System.Threading.Tasks;
using GameService.TCP;
using Microsoft.Extensions.Hosting;

namespace GameApp.Services
{
    public class TcpService : IHostedService
    {
        private readonly ITcpManager _tcpManager;
        
        public TcpService(ITcpManager manager)
        {
            _tcpManager = manager;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _tcpManager.StartServer(3434);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _tcpManager.StopServer();
            return Task.CompletedTask;
        }
    }
}