using MediatR;

namespace Tasks.Cmd.Application.Features.Commands.UpdateTask;

public class UpdateTaskCommand:IRequest
{
    public Guid TaskId { get; set; }
    public Guid AuthorId { get; set; }
    public string? NewTitle { get; set; }
    public string? NewTask { get; set; }
    public DateTime? NewDeadLine { get; set; }
}