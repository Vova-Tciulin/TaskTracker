namespace WebApp.Models.Task;

public class UpdateTaskVm
{
    public Guid TaskId { get; set; }
    public Guid GroupId { get; set; }
    public string Title { get; set; }
    public string Task { get; set; }
    public string DeadLine { get; set; }

    public UpdateTaskVm()
    {
        
    }
}