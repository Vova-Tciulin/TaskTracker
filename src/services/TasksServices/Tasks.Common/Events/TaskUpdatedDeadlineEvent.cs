namespace Tasks.Common.Events;

public class TaskUpdatedDeadlineEvent:BaseEvent
{
    public DateTime NewDeadline { get; set; }
    
    public TaskUpdatedDeadlineEvent()
        : base(nameof(TaskUpdatedDeadlineEvent))
    {
    }
}