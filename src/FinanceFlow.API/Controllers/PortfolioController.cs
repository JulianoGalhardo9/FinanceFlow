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
}

public record CreatePortfolioRequest(string Name);