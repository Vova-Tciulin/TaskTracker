using System.Text.Json;
using EventBus.Messages.Messages;
using MassTransit;
using MediatR;
using Tasks.Cmd.Api.Models;
using Tasks.Cmd.Application.Features.Commands.RemoveTask;
using Tasks.Cmd.Application.Features.Commands.RemoveWorkerFromTasks;

namespace Tasks.Cmd.Api.EventBusConsumers;

public class GroupEventConsumer:IConsumer<EventMessage>
{
    private readonly ILogger<GroupEventConsumer> _logger;
    private readonly IMediator _mediator;


    public GroupEventConsumer(ILogger<GroupEventConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<EventMessage> context)
    {
        _logger.LogInformation($"Received event: {context.Message.EventType}");

        switch (context.Message.EventType)
        {
            case "GroupRemovedEvent":
            {
                var model = JsonSerializer.Deserialize<RemoveGroup>(context.Message.Message);
                
                foreach (var task in model.RemovedTasks)
                {
                    await _mediator.Send(new RemoveTaskCommand() { AuthorId = task.authorId, TaskId = task.taskId });
                }
                break;
            }
            case "GroupUserRemovedEvent":
            {
                var model = JsonSerializer.Deserialize<RemoveUserFromGroup>(context.Message.Message);

                await _mediator.Send(new RemoveWorkerFromTasksCommand()
                {
                    GroupId = model.Id,
                    WorkerId = model.UserId
                });
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