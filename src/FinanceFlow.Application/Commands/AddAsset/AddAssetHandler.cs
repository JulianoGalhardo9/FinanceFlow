using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceFlow.Application.Commands.AddAsset;

public class AddAssetHandler : IRequestHandler<AddAssetCommand, Guid>
{
    private readonly IPortfolioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddAssetHandler> _logger;

    public AddAssetHandler(IPortfolioRepository repository, IUnitOfWork unitOfWork, ILogger<AddAssetHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddAssetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Tentando adicionar ativo {Ticker} na carteira {PortfolioId} para o usuário {UserId}.", 
            request.Ticker, request.PortfolioId, request.UserId);

        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, cancellationToken);

        // 1. Validação de Existência (Com Log)
        if (portfolio is null)
        {
            _logger.LogWarning("Falha ao adicionar ativo: Carteira {PortfolioId} não encontrada.", request.PortfolioId);
            throw new InvalidOperationException($"Carteira com ID {request.PortfolioId} não encontrada.");
        }

        // 2. Validação de Segurança/Dono (Com Log)
        if (portfolio.UserId != request.UserId)
        {
            _logger.LogCritical("TENTATIVA DE ACESSO INDEVIDO: Usuário {IntruderId} tentou alterar carteira do usuário {OwnerId}.", 
                request.UserId, portfolio.UserId);
            throw new InvalidOperationException("Você não tem permissão para alterar esta carteira.");
        }

        // 3. Execução da Lógica de Negócio
        var purchasePrice = new Money(request.PurchasePrice, request.Currency);
        var asset = portfolio.AddAsset(request.Ticker, request.Quantity, purchasePrice);

        // 4. Persistência
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 5. Log de Sucesso
        _logger.LogInformation("Ativo {Ticker} (ID: {AssetId}) adicionado com sucesso à carteira {PortfolioId}.", 
            request.Ticker, asset.Id, request.PortfolioId);

        return asset.Id;
    }
}