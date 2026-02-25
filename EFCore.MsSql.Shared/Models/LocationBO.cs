namespace EFCore.MSSQL.Shared.Models;

public record LocationBO
(
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode
);
