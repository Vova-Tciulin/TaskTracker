using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.ExecuteTask;

public class ExecuteTaskCommandHandler:IRequestHandler<ExecuteTaskCommand>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public ExecuteTaskCommandHandler(ILogger<CreateTaskCommandHandler> logger, IEventSourcingHandler<TaskAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<Unit> Handle(ExecuteTaskCommand request, CancellationToken cancellationToken)
    {
        //TODO: необходимо добавить сервис группы, для проверки, есть ли пользователь в этой группе или нет
        
        _logger.LogInformation($"Handling ExecuteTask with taskId: {request.TaskId}");

        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.TaskId);
        aggregate.TakeTaskInWork(request.WorkerId);

        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return Unit.Value;
    }
}