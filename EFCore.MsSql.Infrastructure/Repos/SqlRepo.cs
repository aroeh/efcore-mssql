using EFCore.MSSQL.Infrastructure.Entities;
using EFCore.MSSQL.Infrastructure.Interfaces;
using EFCore.MSSQL.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.MSSQL.Infrastructure.Repos;

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

    public async Task<IReadOnlyList<TEntity>> CreateManyAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        await SaveAsync();
        return [.. entities];
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

    public async Task<TEntity?> UpdateAsync(string id, Func<TEntity, bool> updateFunction)
    {
        TEntity? entity = await GetAsync(id);

        if (entity is null || !updateFunction(entity))
        {
            return null;
        }

        await SaveAsync();
        return entity;
    }

    public async Task<TEntity?> UpdateAsync(string id, TEntity updateEntity)
    {
        DbSet.Update(updateEntity);
        await SaveAsync();
        return updateEntity;
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities)
    {
        if (entities is null || !entities.Any())
        {
            return;
        }

        DbSet.UpdateRange(entities);
        await SaveAsync();
    }

    public async Task RemoveAsync(string id)
    {
        TEntity? entity = await GetAsync(id);
        if (entity is null)
        {
            return;
        }

        DbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task RemoveManyAsync(string[] ids)
    {
        List<TEntity>? entities = await DbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
        if (entities is null || entities.Count == 0)
        {
            return;
        }

        DbSet.RemoveRange(entities);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // log error and handle
            throw;
        }
    }
}
