using System;
using System.Threading.Tasks;

namespace GameService.TCP.Events
{
    public class GameFinishedEvent : EventBase<GameFinishedEventArgs>
    {
        private GameFinishedEventArgs _args;

        protected override GameFinishedEventArgs Args
        {
            get => _args;
            set => _args = value;
        }

        public override void ResolveDependencies(IServiceProvider serviceProvider)
        {
            
        }

        public override Task Execute()
        {
            return Task.CompletedTask;
        }
    }

    public class GameFinishedEventArgs : EventArgs
    {
        
    }
}