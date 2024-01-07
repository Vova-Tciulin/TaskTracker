using MediatR;

namespace Groups.Cmd.Application.Commands.RemoveTask;

public class RemoveTaskCommand:IRequest<bool>
{
    public Guid GroupId { get; set; }
    public Guid TaskId { get; set; }
}