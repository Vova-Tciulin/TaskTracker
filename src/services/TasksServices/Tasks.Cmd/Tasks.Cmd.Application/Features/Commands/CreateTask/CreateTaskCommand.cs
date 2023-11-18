using MediatR;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Common.enums;

namespace Tasks.Cmd.Application.Features.Commands.CreateTask;

public class CreateTaskCommand:IRequest<TaskAggregate>
{
    public Guid AuthorId { get; set; }
    public Guid GroupId { get; set; }
    public string Title { get; set; }
    public string Task { get; set; }
    public DateTime DeadLine { get; set; }
    
}