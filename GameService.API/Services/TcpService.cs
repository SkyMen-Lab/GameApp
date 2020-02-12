using System.Threading;
using System.Threading.Tasks;
using GameService.TCP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GameApp.Services
{
    public class TcpService : IHostedService
    {
        private readonly ITcpManager _tcpManager;
        private readonly ITcpSettings _settings;
        
        public TcpService(ITcpManager manager, ITcpSettings settings)
        {
            _tcpManager = manager;
            _settings = settings;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _tcpManager.StartServer(_settings.Port);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _tcpManager.StopServer();
            return Task.CompletedTask;
        }
    }
}