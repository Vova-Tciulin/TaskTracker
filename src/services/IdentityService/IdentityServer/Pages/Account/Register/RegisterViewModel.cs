using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Pages.Account.Register;

public class RegisterViewModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string NickName { get; set; }
    [Required]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
    
}