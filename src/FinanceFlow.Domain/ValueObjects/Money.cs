namespace FinanceFlow.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("O valor não pode ser negativo.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("A moeda é obrigatória.", nameof(currency));

        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency.ToUpperInvariant();
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal factor)
    {
        return new Money(Amount * factor, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException(
                $"Não é possível operar entre moedas diferentes: {Currency} e {other.Currency}");
    }

    public bool Equals(Money? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj) => obj is Money money && Equals(money);
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

    public static bool operator ==(Money? left, Money? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Money? left, Money? right) => !(left == right);

    public override string ToString() => $"{Amount:F2} {Currency}";

    public static Money InBRL(decimal amount) => new(amount, "BRL");
    public static Money Zero() => new(0, "BRL");
}