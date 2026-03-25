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

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public async Task<TransactionResult> CreateManyAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        var stateChanges = await SaveAsync();
        return new TransactionResult(true, true, entities.Count(), stateChanges);
    }

    public async Task<IReadOnlyList<TEntity>> QueryAsync(IQueryable<TEntity> query)
    {
        return await query.ToListAsync();
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
    public async Task<PaginationResponse<TEntity>> QueryAsync(IQueryable<TEntity> query, PaginationQueryParametersBO page)
    {
        var totalCount = await DbSet.CountAsync();
        var position = page.Page == 1 ? 0 : (page.Page - 1) * page.PageSize;

        // demonstrates Offset pagination
        var results = await query
            .OrderBy(q => q.Id)
            .Skip(position)
            .Take(page.PageSize)
            .ToListAsync();
        PaginationMetaData metaData = new(page.Page, results.Count, page.PageSize, totalCount);
        return new PaginationResponse<TEntity>(results, metaData);
    }

    public async Task<IReadOnlyList<TEntity>> QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null)
    {
        IQueryable<TEntity> query = QueryBase;

        if (where is not null)
        {
            query = where(query);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetAsync(string id)
    {
        IQueryable<TEntity> query = QueryBase;

        query = query.Where(_ => _.Id.Equals(id));

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TEntity?> GetAsync(string id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null)
    {
        IQueryable<TEntity> query = QueryBase;

        query = query.Where(_ => _.Id.Equals(id));

        if (where is not null)
        {
            query = where(query);
        }

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TransactionResult> UpdateAsync(string id, Func<TEntity, bool> updateFunction)
    {
        TEntity? entity = await GetAsync(id);

        if (entity is null || !updateFunction(entity))
        {
            return new TransactionResult(1);
        }

        var stateChanges = await SaveAsync();
        return new TransactionResult(true, true, 1, stateChanges);
    }

    public async Task<TransactionResult> UpdateAsync(string id, TEntity updateEntity)
    {
        DbSet.Update(updateEntity);
        var stateChanges = await SaveAsync();
        return new TransactionResult(true, true, 1, stateChanges);
    }

    public async Task<TransactionResult> UpdateManyAsync(IEnumerable<TEntity> entities)
    {
        if (entities is null || !entities.Any())
        {
            return new TransactionResult(entities?.Count() ?? 0);
        }

        DbSet.UpdateRange(entities);
        var stateChanges = await SaveAsync();
        return new TransactionResult(true, true, entities.Count(), stateChanges);
    }

    public async Task<TransactionResult> RemoveAsync(string id)
    {
        TEntity? entity = await GetAsync(id);
        if (entity is null)
        {
            return new TransactionResult(1);
        }

        DbSet.Remove(entity);
        var stateChanges = await SaveAsync();
        return new TransactionResult(true, true, 1, stateChanges);
    }

    public async Task<TransactionResult> RemoveManyAsync(string[] ids)
    {
        List<TEntity>? entities = await DbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
        if (entities is null || entities.Count == 0)
        {
            return new TransactionResult(ids.Length);
        }

        DbSet.RemoveRange(entities);
        var stateChanges = await SaveAsync();
        return new TransactionResult(true, true, ids.Length, stateChanges);
    }

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // log error and handle
            throw;
        }
    }
}
