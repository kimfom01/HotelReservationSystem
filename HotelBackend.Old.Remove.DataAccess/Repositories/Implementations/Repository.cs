using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Data;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly Context _context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(Context context)
    {
        _context = context;
        DbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        var added = await DbSet.AddAsync(entity);

        return added.Entity;
    }

    public async Task Delete(int id)
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

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await DbSet.FirstOrDefaultAsync(expression);

        return entity;
    }

    public virtual Task<IQueryable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression)
    {
        var entities = DbSet.Where(expression).AsNoTracking();

        return Task.FromResult(entities);
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetEntity(int id)
    {
        var entity = await DbSet.FindAsync(id);

        return entity;
    }
}
