using MediatR;

namespace Groups.Cmd.Application.Features.Commands.RemoveGroup;

public class RemoveGroupCommand:IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}