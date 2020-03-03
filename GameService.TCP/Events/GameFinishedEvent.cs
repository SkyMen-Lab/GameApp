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

        public async override Task Execute()
        {
            
        }
    }

    public class GameFinishedEventArgs : EventArgs
    {
        
    }
}