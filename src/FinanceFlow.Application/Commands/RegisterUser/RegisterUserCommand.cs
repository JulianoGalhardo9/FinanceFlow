using MediatR;

namespace FinanceFlow.Application.Commands.RegisterUser;
public record RegisterUserCommand(
    string Name,
    string Email,
    string Password
) : IRequest<Guid>;