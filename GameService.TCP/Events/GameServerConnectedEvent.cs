using System;
using System.Linq;
using System.Threading.Tasks;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.TCP.Events
{
    public class GameServerConnectedEvent : EventBase<GameServerConnectedEventArgs>
    {
        private IGameManager _gameManager;
        private MongoRepository _mongoRepository;
        private GameServerConnectedEventArgs _args;

        protected override GameServerConnectedEventArgs Args
        {
            get => _args;
            set => _args = value;
        }

        public override void ResolveDependencies(IServiceProvider serviceProvider)
        {
            _gameManager = serviceProvider.GetRequiredService<IGameManager>();
            _mongoRepository = serviceProvider.GetRequiredService<MongoRepository>();
        }

        public override async Task Execute()
        {
            if (_mongoRepository.GetAll().Any())
            {
                //if a game is present
                var game = _mongoRepository.GetAll().First();
                await _gameManager.SetupGameAsync(game);
            }
        }
    }

    public class GameServerConnectedEventArgs : EventArgs
    {
        
    }
}