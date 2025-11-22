using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register IDbConnection as transient (new connection per request)
        services.AddTransient<IDbConnection>(sp =>
            new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

        // Register generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
