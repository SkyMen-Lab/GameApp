using System;
using System.Threading.Tasks;
using GameService.Domain.DTOs;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace GameService.TCP.Events
{
    public class BallScoredEvent : EventBase<BallScoredEventArgs>
    {
        private MongoRepository _mongoRepository;
        private BallScoredEventArgs _args;

        protected override BallScoredEventArgs Args
        {
            get => _args;
            set => _args = value;
        }

        public override void ResolveDependencies(IServiceProvider serviceProvider)
        {
            _mongoRepository = serviceProvider.GetRequiredService<MongoRepository>();
        }

        public override Task Execute()
        {
            var game = _mongoRepository.GetOne(_args.TeamScored.GameCode);
            if (game != null)
            {
                var filter = _mongoRepository.GetTeamFilter(_args.TeamScored.GameCode, _args.TeamScored.TeamCode);
                var update = Builders<Game>.Update.Inc(
                    x => x.Teams[-1].Score, 1);
                _mongoRepository.Update(filter, update);
            }
            
            return Task.CompletedTask;
        }
    }

    public class BallScoredEventArgs : EventArgs
    {
        //team that has scored the ball
        public TeamDTO TeamScored { get; set; }

        public BallScoredEventArgs(TeamDTO dto)
        {
            TeamScored = dto;
        }
    }
}