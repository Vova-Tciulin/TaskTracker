using MediatR;

namespace Groups.Cmd.Application.Features.Commands.AddUser;

public class AddUserCommand:IRequest<bool>
{
    public string NickNameOrEmail { get; set; }
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
}