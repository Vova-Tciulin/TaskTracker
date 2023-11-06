using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Application.Services;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.CompleteTask;

public class CompleteTaskCommandHandler:IRequestHandler<CompleteTaskCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    

    public CompleteTaskCommandHandler(ILogger<CreateTaskCommandHandler> logger, IEventSourcingHandler<TaskAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
        
    }

    public async Task<Unit> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling completeTask with taskId: {request.TaskId}");
        
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.TaskId);
        aggregate.CompleteTask(request.WorkerId);

        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return Unit.Value;
    }
}