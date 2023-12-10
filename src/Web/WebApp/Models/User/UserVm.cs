namespace WebApp.Models.User;

public class UserVm
{
    public Guid Id { get; set; }
    public string NickName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserVm()
    {
        
    }
}