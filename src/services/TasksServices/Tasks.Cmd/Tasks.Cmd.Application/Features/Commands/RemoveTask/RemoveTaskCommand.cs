using MediatR;

namespace Tasks.Cmd.Application.Features.Commands.RemoveTask;

public class RemoveTaskCommand:IRequest<bool>
{
    public Guid TaskId { get; set; }
    public Guid AuthorId { get; set; }
}