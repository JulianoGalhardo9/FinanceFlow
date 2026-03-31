using MediatR;

namespace FinanceFlow.Application.Commands.RemoveAsset;
public record RemoveAssetCommand(Guid PortfolioId, Guid AssetId, Guid UserId) : IRequest;