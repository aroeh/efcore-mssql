using Microsoft.AspNetCore.Mvc;

namespace EFCore.MSSQL.API.Models;

public record FilterQueryParameters
{
    [FromQuery(Name = "name")]
    public string[]? Names { get; init; } = default!;

    [FromQuery(Name = "cuisine")]
    public string? CuisineType { get; init; } = default!;
}
