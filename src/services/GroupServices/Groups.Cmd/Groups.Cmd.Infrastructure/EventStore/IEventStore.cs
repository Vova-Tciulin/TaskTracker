using Group.Common.Events;

namespace Groups.Cmd.Infrastructure.EventStore;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
    Task<List<BaseEvent>> GetEventsAsync (Guid aggregateId);
}