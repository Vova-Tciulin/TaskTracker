namespace WebApp.Services.ModelDto.Task;

public class CreateTaskDto
{
    public Guid GroupId { get; set; }
    public string Title { get; set; }
    public string Task { get; set; }
    public DateTime DeadLine { get; set; }
}