using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.TCP.Events
{
    /// <summary>
    /// Base class for events
    /// Derive when you want to define a event class to call through <c>EventManager</c>
    /// </summary>
    /// <typeparam name="T">The type of event args</typeparam>
    public abstract class EventBase<T> : IEvent where T : class
    {
        protected abstract T Args { get; set; }
        protected EventHandler EventHandler;
        /// <summary>
        /// Implement to resolve dependencies via <c>IServiceProvider</c>
        /// </summary>
        /// <code>
        ///  _gameManager = serviceProvider.GetRequiredService&lt;IGameManager&gt;();
        /// </code>
        /// <param name="serviceProvider">An interface for restoring dependencies</param>
        public abstract void ResolveDependencies(IServiceProvider serviceProvider);

        /// <summary>
        /// Called when the event is triggered by <c>IEventManager</c>
        /// </summary>
        public abstract Task Execute();
        
        public void SetEvent(EventHandler handler)
        {
            EventHandler = handler;
        }

        public void SetArgs(object args)
        {
            if (args != null)
            {
                Args = args as T;
            }
        }
    }
}