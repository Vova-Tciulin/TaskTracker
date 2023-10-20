namespace Tasks.Common.Events;

public class TaskUpdatedTaskEvent:BaseEvent
{
   
    public string Task { get; set; }

    public TaskUpdatedTaskEvent()
        : base(nameof(TaskUpdatedTaskEvent))
    {
    }
}