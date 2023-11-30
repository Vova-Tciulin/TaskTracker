namespace WebApp.Services.ModelDto.Group;

public class RemoveUserFromGroupDto
{
    public string UserId { get; set; }
    public string GroupId { get; set; }
    public string? AuthorId { get; set; }
}