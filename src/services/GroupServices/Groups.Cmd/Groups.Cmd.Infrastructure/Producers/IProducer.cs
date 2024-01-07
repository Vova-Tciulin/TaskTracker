using Group.Common.Events;

namespace Groups.Cmd.Infrastructure.Producers;

public interface IProducer
{
    Task SendEvent(BaseEvent @event);
}