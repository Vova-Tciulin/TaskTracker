using Tasks.Common.Events;

namespace Tasks.Cmd.Infrastructure.Producers;

public interface IProducer
{
    Task SendEvent(BaseEvent @event);
}