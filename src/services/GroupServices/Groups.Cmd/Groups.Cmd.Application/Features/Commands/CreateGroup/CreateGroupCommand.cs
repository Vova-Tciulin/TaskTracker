using MassTransit.Mediator;
using MediatR;

namespace Groups.Cmd.Application.Features.Commands.CreateGroup;

public class CreateGroupCommand:IRequest<Guid>
{
    public Guid UserId { get; set; }
}