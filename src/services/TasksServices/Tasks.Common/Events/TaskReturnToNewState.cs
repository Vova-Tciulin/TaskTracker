namespace Tasks.Common.Events;

public class TaskReturnToNewState:BaseEvent
{
    public Guid WorkerId { get; set; }
    public TaskReturnToNewState()
        :base(nameof(TaskReturnToNewState))
    {
        
    }    
}