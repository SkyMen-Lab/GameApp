using System;
using GameService.TCP.EventArgs;

namespace GameService.TCP.EventHandling
{
    public interface IEventDisposer
    {
        //TODO: implement subscriber and sender base classes 
        event EventHandler<MovementReceivedEventArgs> OnMovementReceivedEvent;
    }
}