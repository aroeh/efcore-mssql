using EFCore.MSSQL.Infrastructure.Entities;
using EFCore.MSSQL.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.MSSQL.Infrastructure.Repos;

public class SqlRepo<TEntity>
(
    RestuarantDbContext dbContext
) : ISqlRepo<TEntity> where TEntity : EntityBase
{
    private readonly RestuarantDbContext _dbContext = dbContext;
    public DbSet<TEntity> DbSet { get; } = dbContext.Set<TEntity>();

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
        return await query.ToArrayAsync();
    }

    public async Task<IReadOnlyList<TEntity>> QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (where is not null)
        {
            query = where(query);
        }

        return await query.AsNoTracking().ToArrayAsync();
    }

    public async Task<TEntity?> GetAsync(string id)
    {
        IQueryable<TEntity> query = DbSet;

        query = query.Where(_ => _.Id.Equals(id));

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TEntity?> GetAsync(string id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? where = null)
    {
        IQueryable<TEntity> query = DbSet;

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
