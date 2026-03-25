using Demo.Restuarants.Infrastructure.MSSQL.Entities;
using Demo.Restuarants.Infrastructure.MSSQL.Extensions;
using Demo.Restuarants.Infrastructure.MSSQL.Interfaces;
using Demo.Restuarants.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Demo.Restuarants.Infrastructure.MSSQL.Repos;

public class RestuarantRepo
(
    ILogger<RestuarantRepo> log,
    ISqlRepo<RestuarantEntity> sqlRepo
) : IRestuarantRepo
{
    private readonly ILogger<RestuarantRepo> _logger = log;
    private readonly ISqlRepo<RestuarantEntity> _sqlRepo = sqlRepo;

    /// <summary>
    /// Query restuarants
    /// </summary>
    /// <param name="queryParameters">Optional - Query parameters to filter restuarants</param>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
    public async Task<PaginationResponse<RestuarantBO>> QueryRestuarants(FilterQueryParametersBO queryParameters)
    {
        IQueryable<RestuarantEntity> query = _sqlRepo.QueryBase;

        if (queryParameters.Names?.Length > 0)
        {
            query = query.Where(r => queryParameters.Names.Any(n => r.Name.Contains(n)));
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.CuisineType))
        {
            query = query.Where(r => r.CuisineType.Equals(queryParameters.CuisineType));
        }

        var results = await _sqlRepo.QueryAsync(query, queryParameters);
        return new PaginationResponse<RestuarantBO>([.. results.Data.Select(_ => _.ToRestuarantBO())], results.MetaData);
    }

    /// <summary>
    /// Get restuarant by id
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <returns>Restuarant if not <see langword="null"/></returns>
    public async Task<RestuarantBO?> GetRestuarant(string id)
    {
        _logger.LogInformation("Getting restuarant");
        RestuarantEntity? entity = await _sqlRepo.GetAsync(id);
        return entity?.ToRestuarantBO();
    }

    /// <summary>
    /// Creates a new Restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant properties and data</param>
    /// <returns>Restuarant object updated with the new id</returns>
    public async Task<RestuarantBO> CreateRestuarant(RestuarantBO restuarant)
    {
        _logger.LogInformation("Adding new restuarant");
        RestuarantEntity entity = await _sqlRepo.CreateAsync(restuarant.ToRestuarantEntity());
        return entity.ToRestuarantBO();
    }

    /// <summary>
    /// Create many new Restuarants
    /// </summary>
    /// <param name="restuarants">Collection of new restuarants</param>
    /// <returns>MongoDb results for the transaction</returns>
    public async Task<List<RestuarantBO>> CreateManyRestuarants(RestuarantBO[] restuarants)
    {
        _logger.LogInformation("Adding new restuarants");
        RestuarantEntity[] entities = [.. restuarants.Select(_ => _.ToRestuarantEntity())];
        var createdEntities = await _sqlRepo.CreateManyAsync(entities);
        return [.. createdEntities.Select(_ => _.ToRestuarantBO())];
    }

    /// <summary>
    /// Update an existing restuarant
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <param name="request">Restuarant properties to update</param>
    public async Task UpdateRestuarant(string id, UpdateRestuarantRequestBO request)
    {
        _logger.LogInformation("Updating restuarant");

        RestuarantEntity? entity = await _sqlRepo.GetAsync(id);
        if (entity is null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            entity.Name = request.Name;
        }

        if (!string.IsNullOrWhiteSpace(request.CuisineType))
        {
            entity.CuisineType = request.CuisineType;
        }

        if (request.Website is not null)
        {
            entity.Website = request.Website.ToString();
        }

        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            entity.Phone = request.Phone;
        }

        if (request.Address is not null)
        {
            if (!string.IsNullOrWhiteSpace(request.Address.Street))
            {
                entity.Street = request.Address.Street;
            }

            if (!string.IsNullOrWhiteSpace(request.Address.City))
            {
                entity.City = request.Address.City;
            }

            if (!string.IsNullOrWhiteSpace(request.Address.State))
            {
                entity.State = request.Address.State;
            }

            if (!string.IsNullOrWhiteSpace(request.Address.Country))
            {
                entity.Country = request.Address.Country;
            }

            if (!string.IsNullOrWhiteSpace(request.Address.ZipCode))
            {
                entity.ZipCode = request.Address.ZipCode;
            }
        }

        await _sqlRepo.UpdateAsync(id, entity);
    }

    /// <summary>
    /// Removes a restuarant from the database
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    public async Task RemoveRestuarant(string id)
    {
        await _sqlRepo.RemoveAsync(id);
    }
}
