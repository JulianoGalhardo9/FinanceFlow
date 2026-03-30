using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Commands.CreatePortfolio;

public class CreatePortfolioHandler : IRequestHandler<CreatePortfolioCommand, Guid>
{
    private readonly IPortfolioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePortfolioHandler(IPortfolioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = Portfolio.Create(request.UserId, request.Name);

        _repository.Add(portfolio);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return portfolio.Id;
    }
}