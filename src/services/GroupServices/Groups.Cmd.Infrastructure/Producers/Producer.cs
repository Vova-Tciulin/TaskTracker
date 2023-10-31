using System.Text.Json;
using EventBus.Messages.Messages;
using Group.Common.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Infrastructure.Producers;

public class Producer:IProducer
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<Producer> _logger;

    public Producer(IPublishEndpoint publishEndpoint, ILogger<Producer> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task SendEvent(BaseEvent @event)
    {
        var message = new EventMessage()
        {
            EventType = @event.Type,
            Message = JsonSerializer.Serialize(@event, @event.GetType())
        };
            
        _logger.LogInformation($"Publish message {message.EventType} in rabbitMQ.\n Message: {JsonSerializer.Serialize(message)}");
        
        await _publishEndpoint.Publish(message, context=>context.SetRoutingKey("groups."+message.EventType));
    }
}