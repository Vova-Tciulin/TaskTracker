﻿using Tasks.Common.Events;

namespace Tasks.Cmd.Infrastructure.EventStores;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
    Task<List<BaseEvent>> GetEventsAsync (Guid aggregateId);
}