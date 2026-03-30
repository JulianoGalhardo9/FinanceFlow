using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceFlow.Application.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException("Este email já está cadastrado.");

        var hasher = new PasswordHasher<object>();
        var passwordHash = hasher.HashPassword(null!, request.Password);

        var user = User.Create(request.Name, request.Email, passwordHash);

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}