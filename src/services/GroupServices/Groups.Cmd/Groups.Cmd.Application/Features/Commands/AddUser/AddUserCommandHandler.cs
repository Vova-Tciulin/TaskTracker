using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Application.Features.Commands.CreateGroup;
using Groups.Cmd.Application.Services;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Features.Commands.AddUser;

public class AddUserCommandHandler: IRequestHandler<AddUserCommand,bool>
{
    private readonly ILogger<CreateGroupCommandHandler> _logger;
    private readonly IEventSourcingHandler<GroupAggregate> _eventSourcingHandler;
    private readonly IIdentityService _identityService;

    public AddUserCommandHandler(ILogger<CreateGroupCommandHandler> logger, IEventSourcingHandler<GroupAggregate> eventSourcingHandler, IIdentityService identityService)
    {
        _logger = logger;
        _eventSourcingHandler = eventSourcingHandler;
        _identityService = identityService;
    }

    public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling AddUser for groupId: {request.GroupId}");

        var userDto = await _identityService.GetUserByNickNameOrEmail(request.NickNameOrEmail);
        
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.GroupId);
        aggregate.AddUser(request.AuthorId, Guid.Parse(userDto.Id));
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        return true;
    }
}