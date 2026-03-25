using DbUp;
using DbUp.Engine;
using Demo.Restuarants.Infrastructure.MSSQL.Options;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Demo.Restuarants.DbUp.Migration;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json")
            .AddEnvironmentVariables()
            .Build();

        return RunMigrations(configuration);
    }

    static int RunMigrations(IConfiguration configuration)
    {
        var sqlOptions = GetOptions(configuration);

        EnsureDatabase.For.SqlDatabase(sqlOptions.ConnectionString);

        /*
            Scripts in the Run Group Ordered paths will still be run in alphabetical order.  In other words
            The scripts in the Tables directory will be run first and the scripts in that directory will be run in alphabetical order

            **Setting a ScriptType of RunAlways will prevent the scripts from being logged in the Journal table
        */
        var upgrader = DeployChanges.To
            .SqlDatabase(sqlOptions.ConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.Contains("Tables"), new SqlScriptOptions() { RunGroupOrder = 1 })
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.Contains("Data"), new SqlScriptOptions() { RunGroupOrder = 2 })
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed!");
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            Console.ReadLine();
#endif
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }

    private static SqlOptions GetOptions(IConfiguration config)
    {
        var configSettings = config.GetRequiredSection(SqlOptions.ConfigKey);

        var options = configSettings.Get<SqlOptions>();

        if (options is not null)
        {
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                throw new Exception("SQL Connection string is missing");
            }
        }

        return options!;
    }
}
