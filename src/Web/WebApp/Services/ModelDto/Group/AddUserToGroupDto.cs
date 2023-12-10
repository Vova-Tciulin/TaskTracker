namespace WebApp.Services.ModelDto.Group;

public class AddUserToGroupDto
{
    public string NickNameOrEmail { get; set; }
    public string GroupId { get; set; }
    public string AuthorId { get; set; }
}