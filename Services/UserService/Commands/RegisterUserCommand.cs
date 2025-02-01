using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Infrastructure;
using UserService.Infrastructure.Services;
using UserService.Models;

namespace UserService.Commands;
public class RegisterUserCommand : IRequest<AuthResult>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public RegisterUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RabbitMQMessagePublisher _messagePublisher;
    private readonly IJwtService _jwtService;

    public RegisterUserCommandHandler(
        UserManager<IdentityUser> userManager,
        RabbitMQMessagePublisher messagePublisher,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _messagePublisher = messagePublisher;
        _jwtService = jwtService;
    }

    public async Task<AuthResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { Email = request.Email, UserName = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return new AuthResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        // Generate JWT token
        var token = _jwtService.GenerateToken(user);

        // Publish event
        _messagePublisher.PublishUserCreatedEvent(user.Id, user.Email);

        return new AuthResult
        {
            Succeeded = true,
            Token = token,
            UserId = user.Id
        };
    }
}

