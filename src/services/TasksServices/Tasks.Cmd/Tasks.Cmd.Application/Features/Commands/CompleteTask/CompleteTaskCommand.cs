using MediatR;

namespace Tasks.Cmd.Application.Features.Commands.CompleteTask;

public class CompleteTaskCommand:IRequest
{
    public Guid TaskId { get; set; }
    public Guid WorkerId { get; set; }
}