namespace Tasks.Cmd.Application.Models;

public class GroupModel
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public List<UserModel> Users { get; set; }
}