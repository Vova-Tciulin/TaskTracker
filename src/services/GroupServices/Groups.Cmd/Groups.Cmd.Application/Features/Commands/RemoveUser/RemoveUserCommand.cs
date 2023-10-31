using MediatR;

namespace Groups.Cmd.Application.Features.Commands.RemoveUser;

public class RemoveUserCommand:IRequest<bool>
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public Guid AuthorId { get; set; }
}