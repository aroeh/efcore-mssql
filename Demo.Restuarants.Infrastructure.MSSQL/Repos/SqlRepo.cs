using Demo.Restuarants.Infrastructure.MSSQL.Constants;
using Demo.Restuarants.Infrastructure.MSSQL.Entities;
using Demo.Restuarants.Infrastructure.MSSQL.Interfaces;
using Demo.Restuarants.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Restuarants.Infrastructure.MSSQL.Repos;

public class SqlRepo<TEntity>
(
    RestuarantDbContext dbContext
) : ISqlRepo<TEntity> where TEntity : EntityBase
{
    private readonly RestuarantDbContext _dbContext = dbContext;
    private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    public IQueryable<TEntity> QueryBase => _dbContext.Set<TEntity>();

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await SaveAsync(cancellationToken);
        return entity;
    }

    public async Task<TransactionResult> CreateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
        var stateChanges = await SaveAsync(cancellationToken);
        return new TransactionResult(true, true, entities.Count(), stateChanges, DataBaseConstants.Created);
    }

    public async Task<IReadOnlyList<TEntity>> QueryAsync(IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        return await query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Query entities using filters and pagination parameters
    /// </summary>
    /// <param name="query">Query to filter entities</param>
    /// <param name="page">Pagination query parameters</param>
    /// <remarks>
    /// Uses Offset pagination implementation
    /// </remarks>
    /// <returns>Paginated response for entities</returns>
    public async Task<PaginationResponse<TEntity>> QueryAsync(IQueryable<TEntity> query, PaginationQueryParametersBO page, CancellationToken cancellationToken)
    {
        var totalCount = await DbSet.CountAsync(cancellationToken);
        var position = page.Page == 1 ? 0 : (page.Page - 1) * page.PageSize;

        // demonstrates Offset pagination
        var results = await query
            .OrderBy(q => q.Id)
            .Skip(position)
            .Take(page.PageSize)
            .ToListAsync(cancellationToken);
        PaginationMetaData metaData = new(page.Page, results.Count, page.PageSize, totalCount);
        return new PaginationResponse<TEntity>(results, metaData);
    }

    public async Task<IReadOnlyList<TEntity>> QueryAsync(CancellationToken cancellationToken, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null)
    {
        IQueryable<TEntity> query = QueryBase;

        if (where is not null)
        {
            query = where(query);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = QueryBase;

        query = query.Where(_ => _.Id.Equals(id));

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null)
    {
        IQueryable<TEntity> query = QueryBase;

        query = query.Where(_ => _.Id.Equals(id));

        if (where is not null)
        {
            query = where(query);
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TransactionResult> UpdateAsync(string id, CancellationToken cancellationToken, Func<TEntity, bool> updateFunction)
    {
        TEntity? entity = await GetAsync(id, cancellationToken);

        if (entity is null || !updateFunction(entity))
        {
            return new TransactionResult(1, DataBaseConstants.Updated);
        }

        var stateChanges = await SaveAsync(cancellationToken);
        return new TransactionResult(1, stateChanges, DataBaseConstants.Updated);
    }

    public async Task<TransactionResult> UpdateAsync(string id, TEntity updateEntity, CancellationToken cancellationToken)
    {
        var stateChanges = await SaveAsync(cancellationToken);
        return new TransactionResult(1, stateChanges, DataBaseConstants.Updated);
    }

    public async Task<TransactionResult> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        if (entities is null || !entities.Any())
        {
            return new TransactionResult(entities?.Count() ?? 0, DataBaseConstants.Updated);
        }

        DbSet.UpdateRange(entities);
        var stateChanges = await SaveAsync(cancellationToken);
        return new TransactionResult(entities.Count(), stateChanges, DataBaseConstants.Updated);
    }

    public async Task<TransactionResult> RemoveAsync(string id, CancellationToken cancellationToken)
    {
        TEntity? entity = await GetAsync(id, cancellationToken);
        if (entity is null)
        {
            return new TransactionResult(1, DataBaseConstants.Deleted);
        }

        DbSet.Remove(entity);
        var stateChanges = await SaveAsync(cancellationToken);
        return new TransactionResult(1, stateChanges, DataBaseConstants.Deleted);
    }

    public async Task<TransactionResult> RemoveManyAsync(string[] ids, CancellationToken cancellationToken)
    {
        List<TEntity>? entities = await DbSet.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        if (entities is null || entities.Count == 0)
        {
            return new TransactionResult(ids.Length, DataBaseConstants.Deleted);
        }

        DbSet.RemoveRange(entities);
        var stateChanges = await SaveAsync(cancellationToken);
        return new TransactionResult(ids.Length, stateChanges, DataBaseConstants.Deleted);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // log error and handle
            throw;
        }
    }
}
