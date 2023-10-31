namespace Groups.Cmd.Api.Models;

public class AddUserToGroup
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
}