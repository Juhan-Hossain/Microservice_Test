namespace UserService.Models;
public record AuthResponse
{
    public string Token { get; init; }
    public string UserId { get; init; }
    public string Email { get; init; }
    public DateTime ExpiresAt { get; init; }
}