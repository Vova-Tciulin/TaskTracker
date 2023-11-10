namespace TaskTracker.Aggregators.Models;

public class GroupModel
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public List<UserIdResponse> Users { get; set; }=new();
    public List<TaskModel> Tasks { get; set; } = new();
}