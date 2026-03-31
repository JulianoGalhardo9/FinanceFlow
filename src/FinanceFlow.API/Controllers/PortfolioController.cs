using System.Security.Claims;
using FinanceFlow.Application.Commands.AddAsset;
using FinanceFlow.Application.Commands.CreatePortfolio;
using FinanceFlow.Application.Commands.DeletePortfolio;
using FinanceFlow.Application.Commands.RemoveAsset;
using FinanceFlow.Application.Queries.GetPortfolio;
using FinanceFlow.Application.Queries.GetPortfolios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.API.Controllers;

[ApiController]
[Route("api/portfolios")]
[Authorize]
public class PortfolioController : ControllerBase
{
    private readonly ISender _sender;

    public PortfolioController(ISender sender)
    {
        _sender = sender;
    }

    private Guid GetCurrentUserId()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString))
            throw new InvalidOperationException("Usuário não identificado no token.");

        return Guid.Parse(userIdString);
    }

    [HttpGet]
    public async Task<IActionResult> GetPortfolios(CancellationToken cancellationToken)
    {
        var query = new GetPortfoliosQuery(GetCurrentUserId());
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    // Busca os detalhes de uma única carteira por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPortfolio([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPortfolioQuery(id);
        var portfolio = await _sender.Send(query, cancellationToken);

        if (portfolio is null)
            return NotFound(new { message = $"Carteira {id} não encontrada." });

        return Ok(portfolio);
    }

    // Cria uma nova carteira para o usuário logado
    [HttpPost]
    public async Task<IActionResult> CreatePortfolio(
        [FromBody] CreatePortfolioRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new CreatePortfolioCommand(
            UserId: GetCurrentUserId(), 
            Name: request.Name
        );

        var portfolioId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetPortfolio), 
            new { id = portfolioId }, 
            new { id = portfolioId }
        );
    }

    // Adiciona um novo ativo (ação/FII) a uma carteira específica
    [HttpPost("{id}/assets")]
    public async Task<IActionResult> AddAsset(
        [FromRoute] Guid id, 
        [FromBody] AddAssetRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new AddAssetCommand(
            PortfolioId: id,
            UserId: GetCurrentUserId(),
            Ticker: request.Ticker,
            Quantity: request.Quantity,
            PurchasePrice: request.PurchasePrice,
            Currency: request.Currency ?? "BRL"
        );

        var assetId = await _sender.Send(command, cancellationToken);

        return Ok(new { id = assetId });
    }

    // Remove uma carteira inteira do sistema
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePortfolio(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        var command = new DeletePortfolioCommand(id, GetCurrentUserId());
        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    // Remove um assets da carteira
    [HttpDelete("{portfolioId}/assets/{assetId}")]
    public async Task<IActionResult> RemoveAsset(
        [FromRoute] Guid portfolioId, 
        [FromRoute] Guid assetId, 
        CancellationToken cancellationToken)
    {
        var command = new RemoveAssetCommand(portfolioId, assetId, GetCurrentUserId());
        
        await _sender.Send(command, cancellationToken);

        return NoContent(); // HTTP 204
    }
}
public record CreatePortfolioRequest(string Name);

public record AddAssetRequest(
    string Ticker, 
    int Quantity, 
    decimal PurchasePrice, 
    string? Currency
);