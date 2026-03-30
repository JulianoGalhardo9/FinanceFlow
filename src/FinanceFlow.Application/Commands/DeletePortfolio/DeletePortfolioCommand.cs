using MediatR;

namespace FinanceFlow.Application.Commands.DeletePortfolio;

public record DeletePortfolioCommand(Guid PortfolioId, Guid UserId) : IRequest;