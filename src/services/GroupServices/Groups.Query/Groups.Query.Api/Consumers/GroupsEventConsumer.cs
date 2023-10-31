using System.Text.Json;
using EventBus.Messages.Messages;
using Group.Common.Events;
using Groups.Query.Application.EventHandlers;
using MassTransit;

namespace Groups.Query.Api.Consumers;

public class GroupsEventConsumer:IConsumer<EventMessage>
{
    private readonly ILogger<GroupsEventConsumer> _logger;
    private readonly IEventHandler _eventHandler;

    public GroupsEventConsumer(ILogger<GroupsEventConsumer> logger, IEventHandler eventHandler)
    {
        _logger = logger;
        _eventHandler = eventHandler;
    }

    public async Task Consume(ConsumeContext<EventMessage> context)
    {
        _logger.LogInformation($"Received event: {@context.Message.EventType}");

        switch (context.Message.EventType)
        {
            case nameof(GroupCreatedEvent):
            {
                var @event = JsonSerializer.Deserialize<GroupCreatedEvent>(context.Message.Message);
                await _eventHandler.On(@event);
                
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            
            case nameof(GroupRemovedEvent):
            {
                var @event = JsonSerializer.Deserialize<GroupRemovedEvent>(context.Message.Message);
                await _eventHandler.On(@event);
                
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            case nameof(GroupTaskAddedEvent):
            {
                var @event = JsonSerializer.Deserialize<GroupTaskAddedEvent>(context.Message.Message);
                await _eventHandler.On(@event);
                
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            case nameof(GroupTaskRemovedEvent):
            {
                var @event = JsonSerializer.Deserialize<GroupTaskRemovedEvent>(context.Message.Message);
                await _eventHandler.On(@event);
                
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            case nameof(GroupUserAddedEvent):
            {
                var @event = JsonSerializer.Deserialize<GroupUserAddedEvent>(context.Message.Message);
                await _eventHandler.On(@event);
                
                _logger.LogInformation($"Event : {@event.Type} was applied");
                break;
            }
            
            case nameof(GroupUserRemovedEvent):
            {
                var @event = JsonSerializer.Deserialize<GroupUserRemovedEvent>(context.Message.Message);
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