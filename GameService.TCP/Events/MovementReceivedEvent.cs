using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.TCP.Events
{
    public class MovementReceivedEvent : EventBase<MovementReceivedEventArgs>
    {
        private IGameManager _gameManager;

        public override void ResolveDependencies(IServiceProvider serviceProvider)
        {
            _gameManager = serviceProvider.GetRequiredService<IGameManager>();
        }

        public override async Task Execute()
        {
            if (_gameManager != null)
            {
                await _gameManager.MoveThePaddleAsync(Args.Code, Args.Clicks);
                EventHandler?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public class MovementReceivedEventArgs: EventArgs
    {
        private string _code;
        private int _clicks;

        public MovementReceivedEventArgs(string code, int clicks)
        {
            _clicks = clicks;
            _code = code;
        }

        public string Code => _code;

        public int Clicks => _clicks;
    }
}