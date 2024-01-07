using MediatR;

namespace Groups.Cmd.Application.Commands.RemoveGroup;

public class RemoveGroupCommand:IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}