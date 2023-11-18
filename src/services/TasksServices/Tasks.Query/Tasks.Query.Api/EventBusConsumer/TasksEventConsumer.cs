using System.Text.Json;
using EventBus.Messages.Messages;
using MassTransit;
using Tasks.Common.Events;
using Tasks.Query.Application.EventHandlers;

namespace Task.Query.Api.EventBusConsumer;

using System.Threading.Tasks;

public class TasksEventConsumer:IConsumer<EventMessage>
{
    private readonly ILogger<TasksEventConsumer> _logger;
    private readonly IEventHandlers _eventHandler;
    
    public TasksEventConsumer(ILogger<TasksEventConsumer> logger, IEventHandlers eventHandler)
    {
        _logger = logger;
        _eventHandler = eventHandler;
    }

    public async Task Consume(ConsumeContext<EventMessage> context)
    {
        _logger.LogInformation($"Received event: {@context.Message.EventType}");

        switch (context.Message.EventType)
        {
            case "TaskCreatedEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskCreatedEvent>(context.Message.Message);
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            
            case "TaskCompletedEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskCompletedEvent>(context.Message.Message);
                
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            case "TaskRemovedEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskRemovedEvent>(context.Message.Message);
                
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            case "TaskTakenOnWorkEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskTakenOnWorkEvent>(context.Message.Message);
                
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            case "TaskUpdatedDeadlineEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskUpdatedDeadlineEvent>(context.Message.Message);
               
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            
            case "TaskUpdatedTaskEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskUpdatedTaskEvent>(context.Message.Message);
                
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }

            case "TaskUpdatedTitleEvent":
            {
                var @event = JsonSerializer.Deserialize<TaskUpdatedTitleEvent>(context.Message.Message);
                
                await _eventHandler.On(@event);
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            default:
            {
                _logger.LogWarning($"{@context.Message.EventType} doesn't supported");
                break;
            }
        }
    }
}