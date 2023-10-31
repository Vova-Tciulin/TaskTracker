namespace Groups.Cmd.Api.Models;

public class RemoveUserFromGroup
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public Guid AuthorId { get; set; }
}