using MediatR;
using FinanceFlow.Application.DTOs;

namespace FinanceFlow.Application.Queries.GetPortfolios;
public record GetPortfoliosQuery(Guid UserId) : IRequest<IEnumerable<PortfolioDto>>;