using System.Threading.Tasks;
using GameService.TCP.Events;

namespace GameService.TCP.EventHandling
{
    public interface IEventManager
    {
        Task Execute<T>(object args) where T : IEventCommand, new();
    }
}