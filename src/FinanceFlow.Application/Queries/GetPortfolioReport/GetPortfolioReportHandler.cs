using FinanceFlow.Application.DTOs;
using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Queries.GetPortfolioReport;

public class GetPortfolioReportHandler : IRequestHandler<GetPortfolioReportQuery, PortfolioReportDto>
{
    private readonly IPortfolioRepository _repository;

    public GetPortfolioReportHandler(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PortfolioReportDto> Handle(GetPortfolioReportQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, cancellationToken);

        if (portfolio is null)
            throw new InvalidOperationException("Carteira não encontrada.");

        if (portfolio.UserId != request.UserId)
            throw new InvalidOperationException("Acesso negado ao relatório.");

        var totalValue = portfolio.TotalInvested().Amount;

        var allocations = portfolio.Assets
            .GroupBy(a => a.Ticker)
            .Select(group => 
            {
                var investedAmount = group.Sum(a => a.TotalInvested().Amount);
                var totalQuantity = group.Sum(a => a.Quantity);

                return new AssetAllocationDto(
                    Ticker: group.Key,
                    TotalQuantity: totalQuantity,
                    InvestedAmount: investedAmount,
                    AllocationPercentage: totalValue > 0 ? Math.Round((investedAmount / totalValue) * 100, 2) : 0,
                    AveragePrice: totalQuantity > 0 ? Math.Round(investedAmount / totalQuantity, 2) : 0
                );
            })
            .OrderByDescending(a => a.AllocationPercentage)
            .ToList();

        return new PortfolioReportDto(
            portfolio.Id,
            portfolio.Name,
            totalValue,
            allocations
        );
    }
}