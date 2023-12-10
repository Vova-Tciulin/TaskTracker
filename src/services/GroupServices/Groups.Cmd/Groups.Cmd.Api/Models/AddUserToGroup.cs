namespace Groups.Cmd.Api.Models;

public class AddUserToGroup
{
    public string NickNameOrEmail { get; set; }
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
}