using Mssql.Ado.Core.Orchestrations;
using Microsoft.Extensions.DependencyInjection;

namespace Mssql.Ado.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreOrchestrations(this IServiceCollection services)
    {
        services.AddTransient<IRestuarantOrchestration, RestuarantOrchestration>();

        return services;
    }
}