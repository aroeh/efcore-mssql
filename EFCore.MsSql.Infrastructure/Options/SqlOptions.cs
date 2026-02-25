namespace EFCore.MSSQL.Infrastructure.Options;

public record SqlOptions
{
    public const string ConfigKey = "SqlOptions";

    public string? ConnectionString { get; set; }
}
