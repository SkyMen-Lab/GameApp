using System;
using System.Threading.Tasks;
using GameService.TCP.Events;

namespace GameService.TCP.EventHandling
{
    /// <summary>
    /// Global event service for executing events.
    /// </summary>
    public class EventManager : IEventManager
    {
        private readonly IServiceProvider _serviceProvider;

        public EventManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task Execute<T>(object args) where T : IEvent, new()
        {
            var eventCommand = new T();
            eventCommand.SetArgs(args);
            eventCommand.ResolveDependencies(_serviceProvider);
            await eventCommand.Execute();
        }

        public void Subscribe<T>() where T : IEvent, new()
        {
            //TODO: notify other classes about an event
        }
    }
}