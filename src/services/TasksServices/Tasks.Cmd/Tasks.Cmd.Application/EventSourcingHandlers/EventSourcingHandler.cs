using Microsoft.Extensions.Logging;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Cmd.Domain.Exceptions;
using Tasks.Cmd.Infrastructure.EventStores;

namespace Tasks.Cmd.Application.EventSourcingHandlers;

public class EventSourcingHandler:IEventSourcingHandler<TaskAggregate>
{
    private readonly IEventStore _eventStore;
    private readonly ILogger<EventSourcingHandler> _logger;

    public EventSourcingHandler(IEventStore eventStore, ILogger<EventSourcingHandler> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
    }


    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkUncommittedChanges();
    }

    public async Task<TaskAggregate> GetByIdAsync(Guid aggregateId)
    {
        var taskAggregate = new TaskAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (!events.Any())
        {
            _logger.LogError($"No events were found for the aggregate with id: {aggregateId}!");
            throw new NotFoundException($"No events were found for the aggregate with id: {aggregateId}!");
        }
        
        taskAggregate.ReplayEvents(events);
        taskAggregate.Version = events[^1].Version;
        
        return taskAggregate;
    }
}