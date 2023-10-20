using Tasks.Common.enums;

namespace Tasks.Common.Events;

public class TaskCreatedEvent:BaseEvent
{
    public Guid AuthorId { get; set; }
    public Guid GroupId { get; set; }
    
    public string Task { get; set; }
    public DateTime DeadLine { get; set; }
    public TaskState State { get; set; }
    public DateTime TaskCreated { get; set; }
    
    public TaskCreatedEvent() 
        : base(nameof(TaskCreatedEvent))
    {
        
    }
}