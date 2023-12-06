using Tasks.Common.enums;

namespace Tasks.Query.Domain.Models;

public class TaskEntity
{
    public Guid TaskId { get; set; }
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? WorkerId { get; set; }
    public string Title { get; set; }
    public string Task { get; set; }
    public TaskState State { get; set; }
    public DateTime TaskCreated { get; set; }
    public DateTime DeadLine { get; set; }
    public DateTime? CompletedDateTime { get; set; }
    public DateTime? StartOfExecution { get; set; }
}