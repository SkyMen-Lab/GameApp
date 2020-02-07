using System;
using GameService.TCP.EventArgs;

namespace GameService.TCP
{
    public interface IEventDisposer
    {
        event EventHandler<MovementReceivedEventArgs> OnMovementReceivedEvent;
    }
}