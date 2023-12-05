namespace Tasks.Cmd.Api.Models;

public class ReturnTaskToNewDto
{
    public Guid TaskId { get; set; }
    public Guid WorkerId { get; set; }
}