using Application.Contracts;
using Infrastructure.Persistence.DatabaseContext;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDbContext<AuthenticationDatabaseContext>(options =>
            options.UseSqlServer(
                "Server =localhost;" +
                "Database=authentication;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True"
            ));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}