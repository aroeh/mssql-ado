using Mssql.Ado.Infrastructure.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mssql.Ado.Infrastructure.DbService;

namespace Mssql.Ado.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepos(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IRestuarantRepo, RestuarantRepo>();

        services.AddTransient<ISqlHelperService, SqlHelperService>();
        services.AddTransient<ISqlRefHelpers, SqlRefHelpers>();

        return services;
    }
}
