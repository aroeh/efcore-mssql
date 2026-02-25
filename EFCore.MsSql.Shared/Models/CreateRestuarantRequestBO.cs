namespace EFCore.MSSQL.Shared.Models;

public record CreateRestuarantRequestBO
(
    string Name,
    string CuisineType,
    Uri? Website,
    string Phone,
    CreateLocationRequestBO Address
);
