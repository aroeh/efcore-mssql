using Demo.Restuarants.Infrastructure.MSSQL.Entities;
using Demo.Restuarants.Shared.Models;

namespace Demo.Restuarants.Infrastructure.MSSQL.Interfaces;

public interface ISqlRepo<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> QueryBase { get; }

    Task<TEntity> CreateAsync(TEntity entity);

    Task<IReadOnlyList<TEntity>> CreateManyAsync(IEnumerable<TEntity> entities);


    Task<IReadOnlyList<TEntity>> QueryAsync(IQueryable<TEntity> where);

    /// <summary>
    /// Query entities using filters and pagination parameters
    /// </summary>
    /// <param name="query">Query to filter entities</param>
    /// <param name="page">Pagination query parameters</param>
    /// <remarks>
    /// Uses Offset pagination implementation
    /// </remarks>
    /// <returns>Paginated response for entities</returns>
    Task<PaginationResponse<TEntity>> QueryAsync(IQueryable<TEntity> query, PaginationQueryParametersBO page);

    Task<IReadOnlyList<TEntity>> QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null);

    Task<TEntity?> GetAsync(string id);

    Task<TEntity?> GetAsync(string id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null);

    /// <summary>
    /// TODO: This method doesn't work - need to investigate why
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateFunction"></param>
    /// <returns></returns>
    Task<TEntity?> UpdateAsync(string id, Func<TEntity, bool> updateFunction);

    Task<TEntity?> UpdateAsync(string id, TEntity updateEntity);

    Task UpdateManyAsync(IEnumerable<TEntity> entities);

    Task RemoveAsync(string id);

    Task RemoveManyAsync(string[] ids);

    Task SaveAsync();
}
