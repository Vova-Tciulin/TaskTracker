namespace Group.Common.Events;

public class GroupUserAddedEvent:BaseEvent
{
    public Guid UserId { get; set; }
    public GroupUserAddedEvent()
        :base(nameof(GroupUserAddedEvent))
    {
        
    }
}