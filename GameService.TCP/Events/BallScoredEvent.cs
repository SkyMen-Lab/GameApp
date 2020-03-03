using System;
using System.Threading.Tasks;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

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

        public override async Task Execute()
        {
            
        }
    }

    public class BallScoredEventArgs : EventArgs
    {
        //team that has scored the ball
        public Team TeamScored { get; set; }

        public BallScoredEventArgs(Team team)
        {
            TeamScored = team;
        }
    }
}