using System;
using System.Linq;
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
            if (_mongoRepository.GetAll().Any())
            {
                var filter = _mongoRepository.GetCurrentGameTeamFilter(_args.TeamScored.Code);
                var update = Builders<Game>.Update.Set(
                    x => x.Teams[-1].Score, _args.TeamScored.Score);
                _mongoRepository.Update(filter, update);
            }
            
            return Task.CompletedTask;
        }
    }

    public class BallScoredEventArgs : EventArgs
    {
        //team that has scored the ball
        public Team TeamScored { get; set; }

        public BallScoredEventArgs(Team dto)
        {
            TeamScored = dto;
        }
    }
}