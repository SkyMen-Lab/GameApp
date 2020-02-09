using System;
using System.Threading.Tasks;

namespace GameService.TCP.Events
{
    public interface IEventCommand
    {
        Task Execute();
        void SetArgs(object args);
        void ResolveDependencies(IServiceProvider serviceProvider);
    }
}