namespace Tasks.Cmd.Api.Models;

public class RemoveTaskDto
{
    public Guid TaskId { get; set; }
    public Guid AuthorId { get; set; }
}