using MediatR;

namespace FinanceFlow.Application.Commands.AddAsset;
public record AddAssetCommand(
    Guid PortfolioId,
    Guid UserId,
    string Ticker,
    int Quantity,
    decimal PurchasePrice,
    string Currency = "BRL"
) : IRequest<Guid>;