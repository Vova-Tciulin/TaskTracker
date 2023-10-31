namespace Group.Common.Events;

public class GroupUserRemovedEvent:BaseEvent
{
    public Guid UserId { get; set; }

    public GroupUserRemovedEvent()
        :base(nameof(GroupUserRemovedEvent))
    {
        
    }
}