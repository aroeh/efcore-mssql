using EFCore.MSSQL.Core.Extensions;
using EFCore.MSSQL.Core.Interfaces;
using EFCore.MSSQL.Infrastructure.Interfaces;
using EFCore.MSSQL.Shared.Models;
using EFCore.MSSQL.Shared.Utils;
using Microsoft.Extensions.Logging;

namespace EFCore.MSSQL.Core.Orchestrations;

public class RestuarantOrchestration(ILogger<RestuarantOrchestration> log, IRestuarantRepo repo) : IRestuarantOrchestration
{
    private readonly ILogger<RestuarantOrchestration> _logger = log;
    private readonly IRestuarantRepo _repo = repo;

    /// <summary>
    /// List restuarants
    /// </summary>
    /// <param name="queryParameters">Optional - Query parameters to filter restuarants</param>
    /// <returns>List of restuarants matching <paramref name="queryParameters"/></returns>
    public async Task<PaginationResponse<RestuarantBO>> ListRestuarants(FilterQueryParametersBO queryParameters)
    {
        _logger.LogInformation("Querying restuarants");
        return await _repo.QueryRestuarants(queryParameters);
    }

    /// <summary>
    /// Get restuarant by id
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <returns>Restuarant if not <see langword="null"/></returns>
    public async Task<RestuarantBO?> GetRestuarant(string id)
    {
        _logger.LogInformation("Getting restuarant by id");
        return await _repo.GetRestuarant(id);
    }

    /// <summary>
    /// Creates a new Restuarant
    /// </summary>
    /// <param name="request">Restuarant properties and data</param>
    /// <returns>Restuarant object updated with the new id</returns>
    public async Task<RestuarantBO> CreateRestuarant(CreateRestuarantRequestBO request)
    {
        _logger.LogInformation("Adding new restuarant");
        string newId = IdGenerator.GenerateId();
        RestuarantBO restuarant = request.MapToRestuarant(newId);

        return await _repo.CreateRestuarant(restuarant);
    }

    /// <summary>
    /// Create many new Restuarants
    /// </summary>
    /// <param name="requests">Collection of create restuarant requests</param>
    /// <returns>MongoDb results for the transaction</returns>
    public async Task<bool> CreateManyRestuarants(CreateRestuarantRequestBO[] requests)
    {
        RestuarantBO[] requestCollection = new RestuarantBO[requests.Length];
        for (int i = 0; i < requests.Length; i++)
        {
            string newId = IdGenerator.GenerateId();
            RestuarantBO newItem = requests[i].MapToRestuarant(newId);
            requestCollection[i] = newItem;
        }

        _logger.LogInformation("Adding new restuarants");
        var createdEntities = await _repo.CreateManyRestuarants(requestCollection);
        return createdEntities.Count > 0 && createdEntities.Count == requestCollection.Length;
    }

    /// <summary>
    /// Updates a Restuarant record
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <param name="request">Restuarant properties to update</param>
    /// <returns>Success result</returns>
    public async Task UpdateRestuarant(string id, UpdateRestuarantRequestBO request)
    {
        _ = await GetRestuarant(id) ?? throw new Exception("Restuarant does not exist.  Unable to update.");

        _logger.LogInformation("Updating restuarant");
        await _repo.UpdateRestuarant(id, request);
    }

    /// <summary>
    /// Removes a Restuarant record
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    public async Task RemoveRestuarant(string id)
    {
        _logger.LogInformation("Removing restuarant");
        await _repo.RemoveRestuarant(id);
    }
}
