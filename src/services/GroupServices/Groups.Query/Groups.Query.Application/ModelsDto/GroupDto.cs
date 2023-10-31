namespace Groups.Query.Application.ModelsDto;

public class GroupDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public List<UserDto> Users { get; set; }
    public List<TaskDto> Tasks { get; set; }
}