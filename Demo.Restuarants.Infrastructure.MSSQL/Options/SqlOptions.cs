namespace Demo.Restuarants.Infrastructure.MSSQL.Options;

public record SqlOptions
{
    public const string ConfigKey = "SqlOptions";

    public string? ConnectionString { get; set; }
}
