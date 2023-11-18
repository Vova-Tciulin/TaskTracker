namespace Tasks.Common.Events;

public class TaskUpdatedTitleEvent: BaseEvent
{
    public string Title { get; set; }

    public TaskUpdatedTitleEvent()
        : base(nameof(TaskUpdatedTitleEvent))
    {
    }
}