namespace Group.Common.Events;

public class GroupTaskRemovedEvent:BaseEvent
{
    public Guid TaskId { get; set; }
    
    
    public GroupTaskRemovedEvent()
        :base(nameof(GroupTaskRemovedEvent))
    {
        
    }
}