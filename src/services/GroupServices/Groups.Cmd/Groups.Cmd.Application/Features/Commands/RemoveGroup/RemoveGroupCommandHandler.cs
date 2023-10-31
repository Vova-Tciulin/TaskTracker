using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Application.Features.Commands.CreateGroup;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Features.Commands.RemoveGroup;

public class RemoveGroupCommandHandler:IRequestHandler<RemoveGroupCommand,bool>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;

    public RemoveGroupCommandHandler(ILogger<CreateGroupCommandHandler> logger, IEventSourcingHandler<GroupAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<bool> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling RemoveGroup with userId: {request.UserId}");

        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.GroupId);
        aggregate.RemoveGroup(request.UserId);
        
        await _eventSourcingHandler.SaveAsync(aggregate);

        return true;
    }
}