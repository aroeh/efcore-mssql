namespace EFCore.MSSQL.Shared.Models;

public record PaginationQueryParametersBO
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 25;
}
