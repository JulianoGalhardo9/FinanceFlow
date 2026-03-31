using FinanceFlow.Application.Commands.AddAsset;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace FinanceFlow.Domain.Tests.Handlers;

public class AddAssetHandlerTests
{
    private readonly Mock<IPortfolioRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly AddAssetHandler _handler;

    public AddAssetHandlerTests()
    {
        // Criamos as "versões de mentira" das dependências
        _repositoryMock = new Mock<IPortfolioRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        
        // Injetamos os Mocks no Handler real
        _handler = new AddAssetHandler(_repositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_User_Is_Not_Owner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var intruderId = Guid.NewGuid();
        var portfolioId = Guid.NewGuid();

        var portfolio = Portfolio.Create(ownerId, "Carteira do Dono");

        // Configuramos o Mock: "Quando o repositório for chamado com esse ID, retorne esta carteira"
        _repositoryMock.Setup(x => x.GetByIdAsync(portfolioId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(portfolio);

        // Criamos o comando vindo de um usuário intruso
        var command = new AddAssetCommand(portfolioId, "PETR4", 10, 30, "BRL");
    }

    [Fact]
    public async Task Should_Call_Repository_And_Commit_When_Data_Is_Valid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var portfolioId = Guid.NewGuid();
        var portfolio = Portfolio.Create(userId, "Minha Carteira");

        _repositoryMock.Setup(x => x.GetByIdAsync(portfolioId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(portfolio);

        var command = new AddAssetCommand(portfolioId, "VALE3", 5, 90, "BRL");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        // Verificamos se o método SaveChanges foi chamado exatamente UMA vez
        _uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        // Verificamos se o ativo foi realmente inserido na lista do objeto portfolio
        portfolio.Assets.Should().HaveCount(1);
        portfolio.Assets.First().Ticker.Should().Be("VALE3");
    }
}