using MediatR;

namespace Tasks.Cmd.Application.Features.Commands.ExecuteTask;

public class ExecuteTaskCommand:IRequest
{
    public Guid TaskId { get; set; }
    public Guid WorkerId { get; set; }
}