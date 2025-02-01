using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class RegisterUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    public RegisterUserRequest()
    {
    }

    public RegisterUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}