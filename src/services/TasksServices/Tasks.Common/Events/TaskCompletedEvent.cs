namespace Tasks.Common.Events;

public class TaskCompletedEvent:BaseEvent
{
    public Guid WorkerId { get; set; }
    public DateTime CompletedDateTime { get; set; }
    
    public TaskCompletedEvent()
        : base(nameof(TaskCompletedEvent))
    {
    }
}