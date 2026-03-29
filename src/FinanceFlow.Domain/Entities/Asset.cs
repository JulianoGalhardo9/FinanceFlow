using FinanceFlow.Domain.ValueObjects;

namespace FinanceFlow.Domain.Entities;

public class Asset
{
    public Guid Id { get; private set; }
    public Guid PortfolioId { get; private set; }
    public string Ticker { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money PurchasePrice { get; private set; } = null!;
    public DateTime AddedAt { get; private set; }

    private Asset() { }

    public static Asset Create(Guid portfolioId, string ticker, int quantity, Money purchasePrice)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            throw new ArgumentException("O ticker é obrigatório.", nameof(ticker));

        if (ticker.Length < 4 || ticker.Length > 6)
            throw new ArgumentException("Ticker deve ter entre 4 e 6 caracteres.", nameof(ticker));

        if (quantity <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

        return new Asset
        {
            Id = Guid.NewGuid(),
            PortfolioId = portfolioId,
            Ticker = ticker.ToUpperInvariant(),
            Quantity = quantity,
            PurchasePrice = purchasePrice,
            AddedAt = DateTime.UtcNow
        };
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        Quantity = newQuantity;
    }

    public Money TotalInvested()
    {
        return PurchasePrice.Multiply(Quantity);
    }
}