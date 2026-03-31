using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.ValueObjects;
using FluentAssertions;

namespace FinanceFlow.Domain.Tests.Entities;

public class PortfolioTests
{
    [Fact]
    public void Should_Calculate_Total_Invested_Correctly_When_Multiple_Assets_Added()
    {
        // Arrange (Preparar o cenário)
        var userId = Guid.NewGuid();
        var portfolio = Portfolio.Create(userId, "Carteira Teste");
        
        portfolio.AddAsset("PETR4", 10, Money.InBRL(30));

        portfolio.AddAsset("VALE3", 5, Money.InBRL(80));

        // Act (Executar a ação)
        var total = portfolio.TotalInvested();

        total.Amount.Should().Be(700);
        total.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Should_Throw_Error_When_Adding_Duplicate_Ticker()
    {
        // Arrange
        var portfolio = Portfolio.Create(Guid.NewGuid(), "Carteira Teste");
        portfolio.AddAsset("PETR4", 10, Money.InBRL(30));

        // Act
        Action action = () => portfolio.AddAsset("PETR4", 5, Money.InBRL(20));

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*já existe*");
            
    }
}