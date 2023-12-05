using MediatR;

namespace Tasks.Cmd.Application.Features.Commands.RemoveWorkerFromTasks;

public class RemoveWorkerFromTasksCommand:IRequest
{
    public Guid GroupId { get; set; }
    public Guid WorkerId { get; set; }
}