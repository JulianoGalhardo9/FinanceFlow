using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Commands.CreatePortfolio;

public class CreatePortfolioHandler : IRequestHandler<CreatePortfolioCommand, Guid>
{
    private readonly IPortfolioRepository _repository;

    public CreatePortfolioHandler(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = Portfolio.Create(request.UserId, request.Name);

        _repository.Add(portfolio);

        return Task.FromResult(portfolio.Id);
    }
}