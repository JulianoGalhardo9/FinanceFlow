using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.ValueObjects;
using MediatR;

namespace FinanceFlow.Application.Commands.AddAsset;

public class AddAssetHandler : IRequestHandler<AddAssetCommand, Guid>
{
    private readonly IPortfolioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public AddAssetHandler(IPortfolioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddAssetCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, cancellationToken);

        if (portfolio is null)
            throw new InvalidOperationException($"Carteira com ID {request.PortfolioId} não encontrada.");

        // Segurança: Verificamos se o usuário que está tentando adicionar o ativo é o dono da carteira
        if (portfolio.UserId != request.UserId)
        throw new InvalidOperationException("Você não tem permissão para alterar esta carteira.");

        var purchasePrice = new Money(request.PurchasePrice, request.Currency);

        var asset = portfolio.AddAsset(request.Ticker, request.Quantity, purchasePrice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return asset.Id;
    }
}