using MediatR;

namespace FinanceFlow.Application.Commands.CreatePortfolio;

public record CreatePortfolioCommand(Guid UserId, string Name) : IRequest<Guid>;