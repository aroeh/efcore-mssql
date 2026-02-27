namespace EFCore.MSSQL.Shared.Models;

public record PaginationResponse<T>
(
    IEnumerable<T> Data,
    PaginationMetaData MetaData
) where T : class;
