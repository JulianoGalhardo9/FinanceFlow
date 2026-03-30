using FinanceFlow.Application.Commands.LoginUser;
using FinanceFlow.Application.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    // Cria um novo usuário no sistema
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            Name: request.Name,
            Email: request.Email,
            Password: request.Password
        );

        var userId = await _sender.Send(command, cancellationToken);

        return Created(string.Empty, new { id = userId });
    }

    // Autentica o usuário e devolve o token JWT
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(
            Email: request.Email,
            Password: request.Password
        );
        var token = await _sender.Send(command, cancellationToken);

        return Ok(new { token });
    }
}
public record RegisterRequest(string Name, string Email, string Password);

public record LoginRequest(string Email, string Password);