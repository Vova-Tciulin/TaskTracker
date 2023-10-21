using Tasks.Common.Events;

namespace Tasks.Query.Application.EventHandlers;

public interface IEventHandlers
{
    Task On(TaskCreatedEvent @event);
    Task On(TaskCompletedEvent @event);
    Task On(TaskRemovedEvent @event);
    Task On(TaskTakenOnWorkEvent @event);
    Task On(TaskUpdatedDeadlineEvent @event);
    Task On(TaskUpdatedTaskEvent @event);
}