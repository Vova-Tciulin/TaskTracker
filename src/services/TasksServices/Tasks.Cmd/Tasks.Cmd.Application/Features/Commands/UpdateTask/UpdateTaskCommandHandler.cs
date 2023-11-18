using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Application.Services;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.UpdateTask;

public class UpdateTaskCommandHandler: IRequestHandler<UpdateTaskCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly IGroupService _groupService;

    public UpdateTaskCommandHandler(IEventSourcingHandler<TaskAggregate> eventSourcingHandler, ILogger<CreateTaskCommandHandler> logger, IGroupService groupService)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
        _groupService = groupService;
    }

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling UpdateTask with taskId: {request.TaskId}");
        
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.TaskId);

        if (!string.IsNullOrWhiteSpace(request.NewTask))
        {
            _logger.LogInformation($"Update Task with message: {request.NewTask}");
            aggregate.UpdateTask(request.AuthorId, request.NewTask);
        }

        if (request.NewDeadLine!=null)
        {
            _logger.LogInformation($"Update Deadline: {request.NewDeadLine}");
            aggregate.UpdateDeadline(request.AuthorId, request.NewDeadLine.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.NewTitle))
        {
            _logger.LogInformation($"Update Task with message: {request.NewTask}");
            aggregate.UpdateTitle(request.AuthorId, request.NewTitle);
        }
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return Unit.Value;
    }
}