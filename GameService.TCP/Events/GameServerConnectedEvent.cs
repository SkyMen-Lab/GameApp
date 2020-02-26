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
        public override void ResolveDependencies(IServiceProvider serviceProvider)
        {
            _gameManager = serviceProvider.GetRequiredService<IGameManager>();
            _mongoRepository = serviceProvider.GetRequiredService<MongoRepository>();
        }

        public async override Task Execute()
        {
            if (_mongoRepository.GetAll().Any())
            {
                //if no games present

                var game = _mongoRepository.GetAll().First();
                await _gameManager.SetupTeamsAsync(game.Teams);
            }
        }
    }

    public class GameServerConnectedEventArgs : EventArgs
    {
        
    }
}