using System;
using System.Threading.Tasks;

namespace GameService.TCP.Events
{
    public interface IEvent
    {
        Task Execute();
        void SetArgs(object args);
        void ResolveDependencies(IServiceProvider serviceProvider);
    }
}