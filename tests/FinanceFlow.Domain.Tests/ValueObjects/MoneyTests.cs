using FinanceFlow.Domain.ValueObjects;
using FluentAssertions;

namespace FinanceFlow.Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Should_Add_Money_Correct_When_Same_Currency()
    {
        // Arrange (Preparar)
        var m1 = Money.InBRL(100);
        var m2 = Money.InBRL(50);

        // Act (Agir)
        var result = m1.Add(m2);

        // Assert (Verificar)
        result.Amount.Should().Be(150);
        result.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Should_Throw_Error_When_Adding_Different_Currencies()
    {
        // Arrange
        var brl = Money.InBRL(100);
        var usd = new Money(50, "USD");

        // Act
        Action action = () => brl.Add(usd);

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*moedas diferentes*");
    }
}