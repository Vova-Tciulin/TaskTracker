using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Application.Features.Commands.CreateGroup;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Features.Commands.RemoveTask;

public class RemoveTaskCommandHandler:IRequestHandler<RemoveTaskCommand,bool>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;

    public RemoveTaskCommandHandler(IEventSourcingHandler<GroupAggregate> eventSourcingHandler, ILogger<CreateGroupCommandHandler> logger)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
    }

    public async Task<bool> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling RemoveTask from groupId: {request.GroupId}, taskId: {request.TaskId}");

        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.GroupId);
        aggregate.RemoveTask(request.TaskId);

        await _eventSourcingHandler.SaveAsync(aggregate);

        return true;
    }
}