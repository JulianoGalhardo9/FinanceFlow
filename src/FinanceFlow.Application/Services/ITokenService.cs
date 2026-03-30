using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}