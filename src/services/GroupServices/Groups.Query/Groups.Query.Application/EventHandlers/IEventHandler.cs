using Group.Common.Events;

namespace Groups.Query.Application.EventHandlers;

public interface IEventHandler
{
    Task On(GroupCreatedEvent @event);
    Task On(GroupRemovedEvent @event);
    Task On(GroupTaskAddedEvent @event);
    Task On(GroupTaskRemovedEvent @event);
    Task On(GroupUserAddedEvent @event);
    Task On(GroupUserRemovedEvent @event);
}