using MediatR;
using FinanceFlow.Application.DTOs;

namespace FinanceFlow.Application.Queries.GetPortfolio;

public record GetPortfolioQuery(Guid PortfolioId) : IRequest<PortfolioDto?>;