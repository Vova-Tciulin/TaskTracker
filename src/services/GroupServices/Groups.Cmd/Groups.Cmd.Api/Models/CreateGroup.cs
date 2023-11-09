namespace Groups.Cmd.Api.Models;

public class CreateGroup
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
}