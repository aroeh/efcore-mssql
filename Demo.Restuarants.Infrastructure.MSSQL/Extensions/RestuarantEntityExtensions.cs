using Demo.Restuarants.Infrastructure.MSSQL.Entities;
using Demo.Restuarants.Shared.Models;

namespace Demo.Restuarants.Infrastructure.MSSQL.Extensions;

internal static class RestuarantEntityExtensions
{
    internal static RestuarantBO ToRestuarantBO(this RestuarantEntity entity)
    {
        return new RestuarantBO(
            entity.Id,
            entity.Name,
            entity.CuisineType,
            entity.Website is null ? null : new Uri(entity.Website),
            entity.Phone,
            new LocationBO(
                entity.Street,
                entity.City,
                entity.State,
                entity.Country,
                entity.ZipCode
            )
        );
    }

    internal static RestuarantEntity ToRestuarantEntity(this RestuarantBO bo)
    {
        return new RestuarantEntity()
        {
            Id = bo.Id,
            Name = bo.Name,
            CuisineType = bo.CuisineType,
            Website = bo.Website?.ToString(),
            Phone = bo.Phone,
            Street = bo.Address.Street,
            City = bo.Address.City,
            State = bo.Address.State,
            Country = bo.Address.Country,
            ZipCode = bo.Address.ZipCode
        };
    }
}
