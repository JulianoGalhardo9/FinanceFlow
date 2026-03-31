using FinanceFlow.Domain.Repositories;
using MediatR;

namespace FinanceFlow.Application.Commands.RemoveAsset;

public class RemoveAssetHandler : IRequestHandler<RemoveAssetCommand>
{
    private readonly IPortfolioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAssetHandler(IPortfolioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveAssetCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, cancellationToken);

        if (portfolio is null)
            throw new KeyNotFoundException("Carteira não encontrada.");

        if (portfolio.UserId != request.UserId)
            throw new InvalidOperationException("Você não tem permissão para alterar esta carteira.");

        var asset = portfolio.Assets.FirstOrDefault(a => a.Id == request.AssetId);

        if (asset is null)
            throw new KeyNotFoundException("Ativo não encontrado nesta carteira.");

        portfolio.RemoveAsset(request.AssetId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}