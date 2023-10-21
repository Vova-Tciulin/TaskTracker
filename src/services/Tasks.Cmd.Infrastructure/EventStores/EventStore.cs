using System.Text.Json;
using EventBus.Messages.Messages;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Cmd.Domain.Exceptions;
using Tasks.Cmd.Domain.Models;
using Tasks.Cmd.Infrastructure.Contracts;
using Tasks.Common.Events;

namespace Tasks.Cmd.Infrastructure.EventStores;

public class EventStore:IEventStore
{
    private readonly ITaskEventRepository _taskRepository;
    private readonly ILogger<EventStore> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public EventStore(ITaskEventRepository taskRepository, ILogger<EventStore> logger, IPublishEndpoint publishEndpoint)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
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
                AggregateType = nameof(TaskAggregate),
                EventType = @event.Type,
                Version = version,
                Event = @event
            };

            _logger.LogInformation($"Save event {@event.Type} in Db");
            await _taskRepository.SaveAsync(model);

            var message = new BaseMessage()
            {
                Type = @event.GetType().ToString(),
                Message = JsonSerializer.Serialize(@event, @event.GetType())
            };
            
            _logger.LogInformation($"Publish message in rabbitMQ. Message: {JsonSerializer.Serialize(message)}");
            await _publishEndpoint.Publish(message);
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