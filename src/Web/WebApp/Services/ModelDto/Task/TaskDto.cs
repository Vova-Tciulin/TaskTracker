using WebApp.Services.ModelDto.Group;

namespace WebApp.Services.ModelDto.Task;

public class TaskDto
{
    public Guid TaskId { get; set; }
    public Guid GroupId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? WorkerId { get; set; }
    public string Title { get; set; }
    public string Task { get; set; }
    public int State { get; set; }
    public DateTime TaskCreated { get; set; }
    public DateTime DeadLine { get; set; }
    public DateTime? CompletedDateTime { get; set; }
    public UserDto User { get; set; } = new();
}