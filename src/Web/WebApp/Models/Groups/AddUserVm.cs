namespace WebApp.Models.Groups;

public class AddUserVm
{
    public string NickNameOrEmail { get; set; }
    public Guid GroupId { get; set; }

    public AddUserVm()
    {
        
    }
}