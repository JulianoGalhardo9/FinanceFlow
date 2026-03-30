using FinanceFlow.Application.Services;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Infrastructure.Persistence;
using FinanceFlow.Infrastructure.Persistence.Repositories;
using FinanceFlow.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddScoped<IPortfolioRepository, PortfolioRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}