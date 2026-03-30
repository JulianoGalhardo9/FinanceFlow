using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Commands.DeletePortfolio;

public class DeletePortfolioHandler : IRequestHandler<DeletePortfolioCommand>
{
    private readonly IPortfolioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePortfolioHandler(IPortfolioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, cancellationToken);

        if (portfolio is null)
            throw new KeyNotFoundException("Carteira não encontrada.");

        if (portfolio.UserId != request.UserId)
            throw new InvalidOperationException("Você não tem permissão para excluir esta carteira.");

        _repository.Remove(portfolio);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}