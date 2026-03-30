using FinanceFlow.Application.DTOs;
using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Queries.GetPortfolio;

public class GetPortfolioHandler : IRequestHandler<GetPortfolioQuery, PortfolioDto?>
{
    private readonly IPortfolioRepository _repository;

    public GetPortfolioHandler(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PortfolioDto?> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, cancellationToken);

        if (portfolio is null)
            return null;

        return new PortfolioDto(
            portfolio.Id,
            portfolio.Name,
            portfolio.CreatedAt,
            portfolio.TotalInvested().Amount,
            portfolio.Assets.Select(a => new AssetDto(
                a.Id,
                a.Ticker,
                a.Quantity,
                a.PurchasePrice.Amount,
                a.TotalInvested().Amount
            )).ToList()
        );
    }
}