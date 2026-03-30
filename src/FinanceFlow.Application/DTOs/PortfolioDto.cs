namespace FinanceFlow.Application.DTOs;

public record PortfolioDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    decimal TotalInvested,
    IReadOnlyList<AssetDto> Assets
);

public record AssetDto(
    Guid Id,
    string Ticker,
    int Quantity,
    decimal PurchasePrice,
    decimal TotalInvested
);