using FinanceFlow.Domain.ValueObjects;

namespace FinanceFlow.Domain.Entities;

public class Portfolio
{
    private readonly List<Asset> _assets = new();

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyList<Asset> Assets => _assets.AsReadOnly();

    private Portfolio() { }

    public static Portfolio Create(Guid userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome da carteira é obrigatório.", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("O nome não pode ter mais de 100 caracteres.", nameof(name));

        return new Portfolio
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name,
            CreatedAt = DateTime.UtcNow
        };
    }

    public Asset AddAsset(string ticker, int quantity, Money purchasePrice)
    {
        if (_assets.Any(a => a.Ticker == ticker))
            throw new InvalidOperationException($"O ativo {ticker} já existe nessa carteira.");

        var asset = Asset.Create(Id, ticker, quantity, purchasePrice);
        _assets.Add(asset);

        return asset;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("O novo nome é obrigatório.");

        Name = newName;
    }

    public Money TotalInvested()
    {
        if (!_assets.Any())
            return Money.Zero();

        return _assets
            .Select(a => a.TotalInvested())
            .Aggregate((total, next) => total.Add(next));
    }

    public void RemoveAsset(Guid assetId)
{
    var asset = _assets.FirstOrDefault(a => a.Id == assetId);
    
    if (asset is null)
        throw new InvalidOperationException("Ativo não encontrado para remoção.");

    _assets.Remove(asset);
}
}