using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Application.Features.Commands.CreateGroup;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Features.Commands.AddUser;

public class AddUserCommandHandler: IRequestHandler<AddUserCommand,bool>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;

    public AddUserCommandHandler(ILogger<CreateGroupCommandHandler> logger, IEventSourcingHandler<GroupAggregate> eventSourcingHandler)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling AddUser for groupId: {request.GroupId}");
        
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.GroupId);
        aggregate.AddUser(request.AuthorId,request.UserId);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        return true;
    }
}