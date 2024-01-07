using Groups.Cmd.Domain.Aggregates;
using MediatR;

namespace Groups.Cmd.Application.Commands.CreateGroup;

public class CreateGroupCommand:IRequest<GroupAggregate>
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
}