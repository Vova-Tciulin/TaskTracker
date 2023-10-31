using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.UpdateTask;

public class UpdateTaskCommandHandler: IRequestHandler<UpdateTaskCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public UpdateTaskCommandHandler(IEventSourcingHandler<TaskAggregate> eventSourcingHandler, ILogger<CreateTaskCommandHandler> logger)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
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
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return Unit.Value;
    }
}