using Group.Common.Events;
using Groups.Cmd.Domain.Aggregates;
using Groups.Cmd.Domain.Exceptions;
using Groups.Cmd.Domain.Models;
using Groups.Cmd.Infrastructure.Producers;
using Groups.Cmd.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Infrastructure.EventStore;

public class EventStore:IEventStore
{
    private readonly IGroupEventRepository _taskRepository;
    private readonly IProducer _producer;
    private readonly ILogger<EventStore> _logger;

    public EventStore(IGroupEventRepository taskRepository, ILogger<EventStore> logger, IProducer producer)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _producer = producer;
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventModels = await _taskRepository.FindByAggregateId(aggregateId);
        if (expectedVersion!=-1&&expectedVersion!=eventModels[^1].Version)
        {
            throw new IncorrectEventVersion(
                $"task with id: {aggregateId} have different versions. ExpectedVersion: {expectedVersion}," +
                $" but the last event has Version: {eventModels[^1].Version}");
        }

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var model = new EventModel()
            {
                AggregateId = @event.Id,
                EventsCreated = DateTime.Now,
                AggregateType = nameof(GroupAggregate),
                EventType = @event.Type,
                Version = version,
                Event = @event
            };

            _logger.LogInformation($"Save event {@event.Type} in Db");
            await _taskRepository.SaveAsync(model);
            
            await _producer.SendEvent(@event);
        }
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventModels= await _taskRepository.FindByAggregateId(aggregateId);

        if (eventModels==null|| !eventModels.Any())
        {
            _logger.LogError($"no events were found for aggregate with id: {aggregateId}!");
            throw new NotFoundException($"no events were found for aggregate with id: {aggregateId}!");
        }

        return eventModels.OrderBy(u => u.Version).Select(u=>u.Event).ToList();
    }
}