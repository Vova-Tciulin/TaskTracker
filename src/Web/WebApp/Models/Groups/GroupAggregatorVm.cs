using WebApp.Models.Task;
using WebApp.Services.ModelDto.Group;

namespace WebApp.Models.Groups;

public class GroupAggregatorVm
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public List<UserDto> Users { get; set; } = new();
    public List<TaskVm> Tasks { get; set; } = new();
}