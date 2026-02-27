namespace EFCore.MSSQL.Shared.Models;

public record FilterQueryParametersBO
(
    string[]? Names,
    string? CuisineType
) : PaginationQueryParametersBO;
