namespace Tasks.Cmd.Api.Models;

public class ExecuteTaskDto
{
    public Guid TaskId { get; set; }
    public Guid WorkerId { get; set; }
}