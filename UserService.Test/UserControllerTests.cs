using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserService.Commands;
using UserService.Controllers;
using UserService.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace UserService.Test;
public class UsersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Register_ValidRequest_ReturnsOk()
    {
        // Arrange
        RegisterUserRequest request = new RegisterUserRequest("test@example.com", "Password123!");
        var authResult = new AuthResult { Succeeded = true };

        _mediatorMock.Setup(m => m.Send(
            It.IsAny<RegisterUserCommand>(),
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(authResult);

        // Act
        var result = await _controller.Register(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<AuthResult>(okResult.Value);
        Assert.True(returnValue.Succeeded);
    }

    [Fact]
    public async Task Register_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var request = new RegisterUserRequest("test@example.com", "Password123!");
        var authResult = new AuthResult { Succeeded = false, Errors = new[] { "Invalid email" } };

        _mediatorMock.Setup(m => m.Send(
            It.IsAny<RegisterUserCommand>(),
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(authResult);

        // Act
        var result = await _controller.Register(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var returnValue = Assert.IsType<AuthResult>(badRequestResult.Value);
        Assert.False(returnValue.Succeeded);
    }
}