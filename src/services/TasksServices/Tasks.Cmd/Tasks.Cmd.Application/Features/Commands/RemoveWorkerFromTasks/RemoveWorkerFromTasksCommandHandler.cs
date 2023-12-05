using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Application.Services;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Common.enums;

namespace Tasks.Cmd.Application.Features.Commands.RemoveWorkerFromTasks;

public class RemoveWorkerFromTasksCommandHandler:IRequestHandler<RemoveWorkerFromTasksCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly IGroupService _groupService;

    public RemoveWorkerFromTasksCommandHandler(IEventSourcingHandler<TaskAggregate> eventSourcingHandler, ILogger<CreateTaskCommandHandler> logger, IGroupService groupService)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
        _groupService = groupService;
    }

    public async Task<Unit> Handle(RemoveWorkerFromTasksCommand request, CancellationToken cancellationToken)
    {
        var group= await _groupService.GetGroupById(request.GroupId);

        foreach (var tasks in group.Tasks)
        {
            var aggregate=await _eventSourcingHandler.GetByIdAsync(tasks.TaskId);

            if (aggregate.TaskState==TaskState.InWork && aggregate.WorkerId==request.WorkerId)
            {
                _logger.LogInformation($"change task: {aggregate.Id} state to new");
                aggregate.ReturnTaskToNewState(request.WorkerId);
                
                await _eventSourcingHandler.SaveAsync(aggregate);
            }
        }

        return Unit.Value;
    }
}