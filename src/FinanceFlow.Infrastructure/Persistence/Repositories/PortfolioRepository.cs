using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.Persistence.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly AppDbContext _context;

    public PortfolioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .Include(p => p.Assets)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Portfolio>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .Include(p => p.Assets)
            .Where(p => p.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .AnyAsync(p => p.Id == id, cancellationToken);
    }

    public void Add(Portfolio portfolio)
    {
        _context.Portfolios.Add(portfolio);
    }

    public void Remove(Portfolio portfolio)
    {
        _context.Portfolios.Remove(portfolio);
    }
}