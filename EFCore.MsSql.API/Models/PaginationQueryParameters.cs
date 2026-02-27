using Microsoft.AspNetCore.Mvc;

namespace EFCore.MSSQL.API.Models;

public record PaginationQueryParameters
{
    [FromQuery(Name = "page")]
    public int? Page { get; init; } = default!;

    [FromQuery(Name = "pageSize")]
    public int? PageSize { get; init; } = default!;
}
