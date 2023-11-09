namespace Groups.Query.Domain.Entities;

public class GroupEntity
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public List<GroupUser> Users { get; set; } = new();
    public List<GroupTask> Tasks { get; set; } = new();
}