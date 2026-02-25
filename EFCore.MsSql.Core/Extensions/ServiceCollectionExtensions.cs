using EFCore.MSSQL.Core.Interfaces;
using EFCore.MSSQL.Core.Orchestrations;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.MSSQL.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreOrchestrations(this IServiceCollection services)
    {
        services.AddTransient<IRestuarantOrchestration, RestuarantOrchestration>();

        return services;
    }
}