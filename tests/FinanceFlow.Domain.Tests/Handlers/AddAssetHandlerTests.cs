using FinanceFlow.Application.Commands.AddAsset;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;

namespace FinanceFlow.Domain.Tests.Handlers;

public class AddAssetHandlerTests
{
    private readonly Mock<IPortfolioRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<ILogger<AddAssetHandler>> _loggerMock;
    private readonly AddAssetHandler _handler;

    public AddAssetHandlerTests()
    {
        _repositoryMock = new Mock<IPortfolioRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<AddAssetHandler>>();
        _handler = new AddAssetHandler(_repositoryMock.Object, _uowMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Portfolio_Does_Not_Exist()
    {
    // Arrange: Configuramos o banco para retornar NULL (não existe)
    _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync((Portfolio?)null);

    var command = new AddAssetCommand(Guid.NewGuid(), Guid.NewGuid(), "PETR4", 10, 30, "BRL");

    // Act: Executamos o Handler
    Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

    // Assert:
    await action.Should().ThrowAsync<InvalidOperationException>()
        .WithMessage("*não encontrada*");
    }

    [Fact]
    public async Task Should_Throw_Exception_When_User_Is_Not_Owner()
    {
        // Arrange: Criamos um dono e um intruso
        var ownerId = Guid.NewGuid();
        var intruderId = Guid.NewGuid();
        var portfolioId = Guid.NewGuid();
        
        // A carteira pertence ao OWNER
        var portfolio = Portfolio.Create(ownerId, "Carteira do Dono");

        _repositoryMock.Setup(x => x.GetByIdAsync(portfolioId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(portfolio);

        // Comando enviado pelo INTRUSO
        var command = new AddAssetCommand(portfolioId, intruderId, "PETR4", 10, 30, "BRL");

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert: Deve estourar erro de permissão
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Você não tem permissão para alterar esta carteira.");
    }

    [Fact]
    public async Task Should_Add_Asset_Correctly_When_Request_Is_Valid()
    {
        // Arrange: Tudo correto
        var userId = Guid.NewGuid();
        var portfolioId = Guid.NewGuid();
        var portfolio = Portfolio.Create(userId, "Minha Carteira");

        _repositoryMock.Setup(x => x.GetByIdAsync(portfolioId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(portfolio);

        var command = new AddAssetCommand(portfolioId, userId, "VALE3", 5, 90, "BRL");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert: Verificamos se salvou no banco e gerou um ID
        result.Should().NotBeEmpty();
        _uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        portfolio.Assets.Should().HaveCount(1);
        portfolio.Assets.First().Ticker.Should().Be("VALE3");
    }
}