using EFCore.MsSql.Core.Orchestrations;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.MsSql.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreOrchestrations(this IServiceCollection services)
    {
        services.AddTransient<IRestuarantOrchestration, RestuarantOrchestration>();

        return services;
    }
}