using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.CreateTask;
using System.Threading.Tasks;

public class CreateTaskCommandHandler:IRequestHandler<CreateTaskCommand, TaskAggregate>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public CreateTaskCommandHandler(IEventSourcingHandler<TaskAggregate> eventSourcingHandler, ILogger<CreateTaskCommandHandler> logger)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
    }

    public async Task<TaskAggregate> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling CreateTask with authorId: {request.AuthorId}");
        
        var aggregate = new TaskAggregate(Guid.NewGuid(),request.GroupId, request.AuthorId, request.Task,
            request.DeadLine);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return aggregate;
    }
}