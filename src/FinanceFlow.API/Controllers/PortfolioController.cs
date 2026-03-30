using FinanceFlow.Application.Commands.AddAsset;
using FinanceFlow.Application.Commands.CreatePortfolio;
using FinanceFlow.Application.Queries.GetPortfolio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.API.Controllers;

[ApiController]
[Route("api/portfolios")]
public class PortfolioController : ControllerBase
{
    private readonly ISender _sender;

    public PortfolioController(ISender sender)
    {
        _sender = sender;
    }

    // Cria uma nova carteira
    [HttpPost]
    public async Task<IActionResult> CreatePortfolio(
        CreatePortfolioRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePortfolioCommand(
            UserId: Guid.NewGuid(),
            Name: request.Name
        );

        var portfolioId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetPortfolio),
            new { id = portfolioId },
            new { id = portfolioId }
        );
    }

    // Busca uma carteira pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPortfolio(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetPortfolioQuery(id);
        var portfolio = await _sender.Send(query, cancellationToken);

        if (portfolio is null)
            return NotFound(new { message = $"Carteira com ID {id} não encontrada." });

        return Ok(portfolio);
    }

    // Adiciona um ativo dentro de uma carteira existente
    [HttpPost("{id}/assets")]
    public async Task<IActionResult> AddAsset(
        [FromRoute] Guid id,
        [FromBody] AddAssetRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddAssetCommand(
            PortfolioId: id,
            Ticker: request.Ticker,
            Quantity: request.Quantity,
            PurchasePrice: request.PurchasePrice,
            Currency: request.Currency ?? "BRL"
        );

        var assetId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetPortfolio),
            new { id = id },
            new { id = assetId }
        );
    }
}
public record CreatePortfolioRequest(string Name);
public record AddAssetRequest(
    string Ticker,
    int Quantity,
    decimal PurchasePrice,
    string? Currency
);