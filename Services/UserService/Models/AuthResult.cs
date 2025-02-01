namespace UserService.Models;
public class AuthResult
{
    public bool Succeeded { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
}