namespace TaskTracker.Aggregators.Models;

public class GroupResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public List<UserIdResponse> Users { get; set; }=new();
    public List<TaskIdResponse> Tasks { get; set; }=new();
}