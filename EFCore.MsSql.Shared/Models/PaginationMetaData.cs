namespace EFCore.MSSQL.Shared.Models;

public record PaginationMetaData
(
    int CurrentPage,
    int PageRecordCount,
    int PageSize,
    int TotalRecords
)
{
    public int TotalPages => CalculateTotalPages(TotalRecords);

    private int CalculateTotalPages(int totalRecords)
    {
        return (int)Math.Ceiling((decimal)totalRecords/PageSize);
    }
}
