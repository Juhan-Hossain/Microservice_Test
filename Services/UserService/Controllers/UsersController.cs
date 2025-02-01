using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UserService.Commands;
using UserService.Models;
using UserService.Queries;

namespace UserService.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Microsoft.AspNetCore.Identity.Data.LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : Unauthorized();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);
        return user != null ? Ok(user) : NotFound();
    }
}