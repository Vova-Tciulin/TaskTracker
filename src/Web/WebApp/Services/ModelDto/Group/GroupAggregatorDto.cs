using WebApp.Services.ModelDto.Task;

namespace WebApp.Services.ModelDto.Group;

public class GroupAggregatorDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public List<UserDto> Users { get; set; } = new();
    public List<TaskDto> Tasks { get; set; } = new();
}