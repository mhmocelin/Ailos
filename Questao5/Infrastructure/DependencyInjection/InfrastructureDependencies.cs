using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.DependencyInjection;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new DatabaseConfig
        {
            Name = configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite")
        });

        services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

        services.AddScoped<IDbConnection>(sp =>
        {
            var config = sp.GetRequiredService<DatabaseConfig>();
            var connection = new SqliteConnection(config.Name);
            connection.Open(); // <- importante abrir a conexão aqui
            return connection;
        });

        return services;
    }
}