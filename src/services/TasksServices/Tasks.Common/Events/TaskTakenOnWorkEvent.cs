namespace Tasks.Common.Events;

public class TaskTakenOnWorkEvent:BaseEvent
{
    public Guid WorkerId { get; set; }
    public DateTime DateTime { get; set; }
    
    public TaskTakenOnWorkEvent()
        :base(nameof(TaskTakenOnWorkEvent))
    {
        
    }
}