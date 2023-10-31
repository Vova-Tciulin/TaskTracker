namespace Group.Common.Events;

public class GroupTaskAddedEvent:BaseEvent 
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    
    public GroupTaskAddedEvent()
        :base(nameof(GroupTaskAddedEvent))
    {
        
    }
}