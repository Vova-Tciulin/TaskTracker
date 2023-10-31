namespace Groups.Cmd.Api.Models;

public class RemoveGroup
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}