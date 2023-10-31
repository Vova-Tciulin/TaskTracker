namespace Group.Common.Events;

public class GroupCreatedEvent:BaseEvent
{
    public Guid AuthorId { get; set; }
    public GroupCreatedEvent()
        :base(nameof(GroupCreatedEvent))
    {
        
    }
}