using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Portfolio>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Portfolio portfolio);
    void Remove(Portfolio portfolio);
}