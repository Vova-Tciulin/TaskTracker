namespace Tasks.Common.Events;

public class TaskRemovedEvent:BaseEvent
{
    public TaskRemovedEvent() : base(nameof(TaskRemovedEvent))
    {
    }
}