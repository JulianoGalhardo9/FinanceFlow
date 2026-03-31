using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit; // Adicionado explicitamente

namespace FinanceFlow.Integration.Tests.Controllers;

public class PortfolioControllerTests : IClassFixture<FinanceFlowWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PortfolioControllerTests(FinanceFlowWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePortfolio_ShouldReturnUnauthorized_WhenNoTokenIsProvided()
    {
        // Arrange
        var request = new { Name = "Carteira de Integração" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/portfolios", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}