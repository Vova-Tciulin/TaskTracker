namespace Groups.Query.Domain.Entities;

public class GroupUser
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}