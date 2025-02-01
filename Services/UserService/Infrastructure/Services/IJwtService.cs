using Microsoft.AspNetCore.Identity;

namespace UserService.Infrastructure.Services;

public interface IJwtService
{
    string GenerateToken(IdentityUser user);
}
