using Questao5.Infrastructure.Repositories;
using Questao5.Infrastructure.Repositories.Interfaces;

namespace Questao5.Infrastructure.DependencyInjection;

public static class RepositoryDependencies
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
        services.AddScoped<ISaldoContaCorrenteRepository, SaldoContaCorrenteRepository>();

        return services;
    }
}
