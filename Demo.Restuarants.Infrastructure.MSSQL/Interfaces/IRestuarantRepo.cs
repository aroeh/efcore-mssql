using Demo.Restuarants.Shared.Models;

namespace Demo.Restuarants.Infrastructure.MSSQL.Interfaces;

public interface IRestuarantRepo
{
    /// <summary>
    /// Query restuarants
    /// </summary>
    /// <param name="queryParameters">Optional - Query parameters to filter restuarants</param>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
    Task<PaginationResponse<RestuarantBO>> QueryRestuarants(FilterQueryParametersBO queryParameters);

    /// <summary>
    /// Get restuarant by id
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <returns>Restuarant if not <see langword="null"/></returns>
    Task<RestuarantBO?> GetRestuarant(string id);

    /// <summary>
    /// Creates a new Restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant properties and data</param>
    /// <returns>Restuarant object updated with the new id</returns>
    Task<RestuarantBO> CreateRestuarant(RestuarantBO restuarant);

    /// <summary>
    /// Create many new Restuarants
    /// </summary>
    /// <param name="restuarants">Collection of new restuarants</param>
    /// <returns>Results for the transaction</returns>
    Task<TransactionResult> CreateManyRestuarants(RestuarantBO[] restuarants);

    /// <summary>
    /// Update an existing restuarant
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <param name="request">Restuarant properties to update</param>
    /// <returns>Results for the transaction</returns>
    Task<TransactionResult> UpdateRestuarant(string id, UpdateRestuarantRequestBO request);

    /// <summary>
    /// Removes a restuarant from the database
    /// </summary>
    /// <param name="id">Id of the restuarant</param>
    /// <returns>Results for the transaction</returns>
    Task<TransactionResult> RemoveRestuarant(string id);
}
