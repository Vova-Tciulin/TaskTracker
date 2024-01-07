using Groups.Cmd.Domain.Aggregates;
using Groups.Cmd.Domain.Exceptions;
using Groups.Cmd.Infrastructure.EventStore;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.EventSourcingHandlers;


public class EventSourcingHandler:IEventSourcingHandler<GroupAggregate>
{
    private readonly IEventStore _eventStore;
    private readonly ILogger<EventSourcingHandler> _logger;

    public EventSourcingHandler(IEventStore eventStore, ILogger<EventSourcingHandler> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
    }


    /// <summary>
    /// сохраняет новые события аггрегата 
    /// </summary>
    public async Task SaveAsync(AggregateRoot aggregate)
    {
        _logger.LogInformation($"aggregateId: {aggregate.Id}");
        
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkUncommittedChanges();
    }

    /// <summary>
    /// получение событий аггрегата, их воспроизведение и возврат аггрегата
    /// </summary>
    public async Task<GroupAggregate> GetByIdAsync(Guid aggregateId)
    {
        var taskAggregate = new GroupAggregate();
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