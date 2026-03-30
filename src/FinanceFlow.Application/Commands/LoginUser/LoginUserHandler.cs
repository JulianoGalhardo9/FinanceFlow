using FinanceFlow.Application.Services;
using FinanceFlow.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceFlow.Application.Commands.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginUserHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            throw new InvalidOperationException("Email ou senha inválidos.");

        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(null!, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("Email ou senha inválidos.");

        return _tokenService.GenerateToken(user);
    }
}