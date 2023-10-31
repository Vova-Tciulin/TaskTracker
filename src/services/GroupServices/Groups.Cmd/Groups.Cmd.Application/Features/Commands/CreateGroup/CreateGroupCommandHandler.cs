using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Features.Commands.CreateGroup;

public class CreateGroupCommandHandler:IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;

    public CreateGroupCommandHandler(ILogger<CreateGroupCommandHandler> logger, IEventSourcingHandler<GroupAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling CreateGroup with userId: {request.UserId}");
        
        var aggregate = new GroupAggregate(Guid.NewGuid(), request.UserId);
        await _eventSourcingHandler.SaveAsync(aggregate);

        return aggregate.Id;
    }
}