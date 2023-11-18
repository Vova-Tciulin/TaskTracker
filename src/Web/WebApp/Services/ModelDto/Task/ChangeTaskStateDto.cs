namespace WebApp.Services.ModelDto.Task;

public class ChangeTaskStateDto
{
    public Guid TaskId { get; set; }
    public Guid WorkerId { get; set; }
}