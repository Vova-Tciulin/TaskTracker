using Groups.Cmd.Application.Commands.CreateGroup;
using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Commands.AddTask;

public class AddTaskCommandHandler:IRequestHandler<AddTaskCommand,bool>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;

    public AddTaskCommandHandler(IEventSourcingHandler<GroupAggregate> eventSourcingHandler, ILogger<CreateGroupCommandHandler> logger)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
    }

    public async Task<bool> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling AddTask for groupId: {request.GroupId}");

        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.GroupId);
        aggregate.AddTask(request.UserId,request.TaskId);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        return true;
    }
}