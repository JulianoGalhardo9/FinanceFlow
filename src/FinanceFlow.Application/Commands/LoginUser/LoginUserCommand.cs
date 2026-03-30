using MediatR;

namespace FinanceFlow.Application.Commands.LoginUser;
public record LoginUserCommand(
    string Email,
    string Password
) : IRequest<string>;