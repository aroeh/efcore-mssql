using EFCore.MSSQL.Infrastructure.Entities;
using EFCore.MSSQL.Infrastructure.Interfaces;
using EFCore.MSSQL.Infrastructure.Options;
using EFCore.MSSQL.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.MSSQL.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepos(this IServiceCollection services, IConfiguration config)
    {
        var sqlOptions = GetOptions(services, config);

        services.AddDbContext<RestuarantDbContext>(
            options => options.UseSqlServer(sqlOptions.ConnectionString)
        );

        services.AddScoped<ISqlRepo<RestuarantEntity>, SqlRepo<RestuarantEntity>>();
        services.AddScoped<IRestuarantRepo, RestuarantRepo>();

        return services;
    }

    private static SqlOptions GetOptions(IServiceCollection services, IConfiguration config)
    {
        var configSettings = config.GetRequiredSection(SqlOptions.ConfigKey);

        var options = configSettings.Get<SqlOptions>();

        if (options is not null)
        {
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                throw new Exception("MongoDb Connection string is missing");
            }
        }

        services.Configure<SqlOptions>(configSettings);

        return options!;
    }
}
