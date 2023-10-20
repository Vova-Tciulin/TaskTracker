using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Cmd.Infrastructure.Contracts;

namespace Tasks.Cmd.Application.Features.Commands.RemoveTask;

public class RemoveTaskCommandHandler:IRequestHandler<RemoveTaskCommand, bool>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public RemoveTaskCommandHandler(ILogger<CreateTaskCommandHandler> logger, IEventSourcingHandler<TaskAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<bool> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling RemoveTask with taskId: {request.TaskId}");

        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.TaskId);
        aggregate.RemoveTask(request.AuthorId);

        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return true;
    }
}