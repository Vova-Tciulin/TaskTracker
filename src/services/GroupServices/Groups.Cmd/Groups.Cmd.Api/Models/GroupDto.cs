namespace Groups.Cmd.Api.Models;

public class GroupDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
}