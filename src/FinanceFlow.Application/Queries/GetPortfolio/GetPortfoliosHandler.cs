using FinanceFlow.Application.DTOs;
using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Queries.GetPortfolios;

public class GetPortfoliosHandler : IRequestHandler<GetPortfoliosQuery, IEnumerable<PortfolioDto>>
{
    private readonly IPortfolioRepository _repository;

    public GetPortfoliosHandler(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PortfolioDto>> Handle(GetPortfoliosQuery request, CancellationToken cancellationToken)
    {
        var portfolios = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);

        return portfolios.Select(p => new PortfolioDto(
            p.Id,
            p.Name,
            p.CreatedAt,
            p.TotalInvested().Amount,
            p.Assets.Select(a => new AssetDto(
                a.Id,
                a.Ticker,
                a.Quantity,
                a.PurchasePrice.Amount,
                a.TotalInvested().Amount
            )).ToList()
        ));
    }
}