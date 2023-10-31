namespace Tasks.Common.Events;

public class TaskRemovedEvent:BaseEvent
{
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
    public TaskRemovedEvent() : base(nameof(TaskRemovedEvent))
    {
    }
}