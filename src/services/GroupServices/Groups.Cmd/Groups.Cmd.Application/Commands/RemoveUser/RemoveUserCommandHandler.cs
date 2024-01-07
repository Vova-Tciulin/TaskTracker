using Groups.Cmd.Application.Commands.CreateGroup;
using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Commands.RemoveUser;

public class RemoveUserCommandHandler: IRequestHandler<RemoveUserCommand,bool>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;

    public RemoveUserCommandHandler(ILogger<CreateGroupCommandHandler> logger, IEventSourcingHandler<GroupAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling RemoveUser for groupId: {request.GroupId}");

        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.GroupId);
        aggregate.RemoveUser(request.AuthorId,request.UserId);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        return true;
    }
}