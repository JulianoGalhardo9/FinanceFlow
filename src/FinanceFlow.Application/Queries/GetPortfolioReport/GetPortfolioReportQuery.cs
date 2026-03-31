using MediatR;
using FinanceFlow.Application.DTOs;

namespace FinanceFlow.Application.Queries.GetPortfolioReport;
public record GetPortfolioReportQuery(Guid PortfolioId, Guid UserId) : IRequest<PortfolioReportDto>;