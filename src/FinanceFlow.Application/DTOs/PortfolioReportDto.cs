namespace FinanceFlow.Application.DTOs;

public record PortfolioReportDto(
    Guid PortfolioId,
    string PortfolioName,
    decimal TotalValue,
    List<AssetAllocationDto> Allocations
);

public record AssetAllocationDto(
    string Ticker,
    int TotalQuantity,
    decimal InvestedAmount,
    decimal AllocationPercentage,
    decimal AveragePrice
);