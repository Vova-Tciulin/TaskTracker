using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Application.Services;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.ExecuteTask;

public class ExecuteTaskCommandHandler:IRequestHandler<ExecuteTaskCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly IGroupService _groupService;

    public ExecuteTaskCommandHandler(ILogger<CreateTaskCommandHandler> logger, IEventSourcingHandler<TaskAggregate> eventSourcingHandler, IGroupService groupService)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
        _groupService = groupService;
    }

    public async Task<Unit> Handle(ExecuteTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling ExecuteTask with taskId: {request.TaskId}");
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.TaskId);
        
        var group = await _groupService.GetGroupById(aggregate.GroupId);
        if (!group.Users.Exists(u=>u.UserId==request.WorkerId))
        {
            _logger.LogWarning(
                $"user with id: {request.WorkerId} isn't a member of the group with id: {request.WorkerId}");
            
            throw new InvalidOperationException(
                $"user with id: {request.WorkerId} isn't a member of the group with id: {request.WorkerId}");
        }
        
        aggregate.TakeTaskInWork(request.WorkerId);

        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return Unit.Value;
    }
}