using System.Text.Json;
using EventBus.Messages.Messages;
using Groups.Cmd.Api.Models;
using Groups.Cmd.Application.Features.Commands.AddTask;
using Groups.Cmd.Application.Features.Commands.RemoveTask;
using MassTransit;
using MediatR;

namespace Groups.Cmd.Api.EventBusConsumers;

public class TasksEventConsumer:IConsumer<EventMessage>
{
    private readonly ILogger<TasksEventConsumer> _logger;
    private readonly IMediator _mediator;
    
    public TasksEventConsumer(ILogger<TasksEventConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<EventMessage> context)
    {
        _logger.LogInformation($"Received event: {@context.Message.EventType}");

        switch (context.Message.EventType)
        {
            case "TaskCreatedEvent":
            {
                var model = JsonSerializer.Deserialize<AddTask>(context.Message.Message);
                
                var res= await _mediator.Send(new AddTaskCommand()
                    { GroupId = model.GroupId, TaskId = model.Id, UserId = model.AuthorId });
                
                if (res)
                {
                    _logger.LogInformation($"Task {model.Id} was added to group {model.GroupId}");
                }
                break;
            }
            case "TaskRemovedEvent":
            {
                var model = JsonSerializer.Deserialize<RemoveTask>(context.Message.Message);

                var res = await _mediator.Send(new RemoveTaskCommand() { GroupId = model.GroupId, TaskId = model.Id });
                
                if (res)
                {
                    _logger.LogInformation($"Task {model.Id} was removed from group {model.GroupId}");
                }
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