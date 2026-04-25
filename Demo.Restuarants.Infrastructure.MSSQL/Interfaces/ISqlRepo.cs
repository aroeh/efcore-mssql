using Demo.Restuarants.Infrastructure.MSSQL.Entities;
using Demo.Restuarants.Shared.Models;

namespace Demo.Restuarants.Infrastructure.MSSQL.Interfaces;

public interface ISqlRepo<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> QueryBase { get; }

    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TransactionResult> CreateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);


    Task<IReadOnlyList<TEntity>> QueryAsync(IQueryable<TEntity> where, CancellationToken cancellationToken);

    /// <summary>
    /// Query entities using filters and pagination parameters
    /// </summary>
    /// <param name="query">Query to filter entities</param>
    /// <param name="page">Pagination query parameters</param>
    /// <param name="cancellationToken">Token for handling cancellation requests</param>
    /// <remarks>
    /// Uses Offset pagination implementation
    /// </remarks>
    /// <returns>Paginated response for entities</returns>
    Task<PaginationResponse<TEntity>> QueryAsync(IQueryable<TEntity> query, PaginationQueryParametersBO page, CancellationToken cancellationToken);

    Task<IReadOnlyList<TEntity>> QueryAsync(CancellationToken cancellationToken, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null);

    Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken);

    Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null);

    /// <summary>
    /// TODO: This method doesn't work - need to investigate why
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Token for handling cancellation requests</param>
    /// <param name="updateFunction"></param>
    /// <returns></returns>
    Task<TransactionResult> UpdateAsync(string id, CancellationToken cancellationToken, Func<TEntity, bool> updateFunction);

    Task<TransactionResult> UpdateAsync(string id, TEntity updateEntity, CancellationToken cancellationToken);

    Task<TransactionResult> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task<TransactionResult> RemoveAsync(string id, CancellationToken cancellationToken);

    Task<TransactionResult> RemoveManyAsync(string[] ids, CancellationToken cancellationToken);

    Task<int> SaveAsync(CancellationToken cancellationToken);
}
