using System.Linq.Expressions;
using HotelBackend.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Data.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(DatabaseContext databaseContext)
    {
        DbSet = databaseContext.Set<TEntity>();
    }

    public async Task<TEntity?> Add(TEntity entity)
    {
        var added = await DbSet.AddAsync(entity);

        return added.Entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity is not null)
        {
            DbSet.Remove(entity);
        }
    }

    public Task Update(TEntity entity)
    {
        DbSet.Update(entity);

        return Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression)
    {
        return await Task.Run(() => DbSet.Where(expression).AsNoTracking());
    }

    public virtual async Task<TEntity?> GetEntity(Guid id)
    {
        var entity = await DbSet.FindAsync(id);

        return entity;
    }

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await DbSet.FirstOrDefaultAsync(expression);

        return entity;
    }
}