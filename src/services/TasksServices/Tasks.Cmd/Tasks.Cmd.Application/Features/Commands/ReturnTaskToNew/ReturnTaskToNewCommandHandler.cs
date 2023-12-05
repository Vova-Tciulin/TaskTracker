using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.ReturnTaskToNew;

public class ReturnTaskToNewCommandHandler:IRequestHandler<ReturnTaskToNewCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public ReturnTaskToNewCommandHandler(IEventSourcingHandler<TaskAggregate> eventSourcingHandler, ILogger<CreateTaskCommandHandler> logger)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
    }

    public async Task<Unit> Handle(ReturnTaskToNewCommand request, CancellationToken cancellationToken)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.TaskId);
        aggregate.ReturnTaskToNewState(request.WorkerId);

        await _eventSourcingHandler.SaveAsync(aggregate);

        return Unit.Value;
    }
}