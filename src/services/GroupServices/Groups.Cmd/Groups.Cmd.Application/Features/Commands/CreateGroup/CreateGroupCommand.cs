using Groups.Cmd.Domain.Aggregates;
using MassTransit.Mediator;
using MediatR;

namespace Groups.Cmd.Application.Features.Commands.CreateGroup;

public class CreateGroupCommand:IRequest<GroupAggregate>
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
}