namespace Groups.Cmd.Api.Models;

public class RemoveTask
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
}