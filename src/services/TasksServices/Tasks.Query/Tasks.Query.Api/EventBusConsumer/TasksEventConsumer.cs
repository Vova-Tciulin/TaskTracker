using System.Text.Json;
using EventBus.Messages.Messages;
using MassTransit;
using Tasks.Common.Events;
using Tasks.Query.Application.EventHandlers;

namespace Task.Query.Api.EventBusConsumer;

using System.Threading.Tasks;

public class TasksEventConsumer:IConsumer<BaseMessage>
{
    private readonly ILogger<TasksEventConsumer> _logger;
    private readonly IEventHandlers _eventHandler;
    
    public TasksEventConsumer(ILogger<TasksEventConsumer> logger, IEventHandlers eventHandler)
    {
        _logger = logger;
        _eventHandler = eventHandler;
    }

    public async Task Consume(ConsumeContext<BaseMessage> context)
    {
        _logger.LogInformation($"Received event: {@context.Message.Type}");

        switch (context.Message.Type)
        {
            case "Tasks.Common.Events.TaskCreatedEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskCreatedEvent>(context.Message.Message);
                _logger.LogInformation($"events: {JsonSerializer.Serialize(@event)}");
                await _eventHandler.On(@event);
                break;
            }
            
            case "Tasks.Common.Events.TaskCompletedEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskCompletedEvent>(context.Message.Message);
                _logger.LogInformation($"events: {JsonSerializer.Serialize(@event)}");
                await _eventHandler.On(@event);
                break;
            }
            case "Tasks.Common.Events.TaskRemovedEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskRemovedEvent>(context.Message.Message);
                _logger.LogInformation($"events: {JsonSerializer.Serialize(@event)}");
                await _eventHandler.On(@event);
                break;
            }
            case "Tasks.Common.Events.TaskTakenOnWorkEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskTakenOnWorkEvent>(context.Message.Message);
                _logger.LogInformation($"events: {JsonSerializer.Serialize(@event)}");
                await _eventHandler.On(@event);
                break;
            }
            case "Tasks.Common.Events.TaskUpdatedDeadlineEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskUpdatedDeadlineEvent>(context.Message.Message);
                _logger.LogInformation($"events: {JsonSerializer.Serialize(@event)}");
                await _eventHandler.On(@event);
                break;
            }
            
            case "Tasks.Common.Events.TaskUpdatedTaskEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskUpdatedTaskEvent>(context.Message.Message);
                _logger.LogInformation($"events: {JsonSerializer.Serialize(@event)}");
                await _eventHandler.On(@event);
                break;
            }

            default:
            {
                _logger.LogWarning($"{@context.Message.Type} doesn't supported");
                break;
            }
        }
    }
}