using System.Linq.Expressions;
using HotelBackend.Reservations.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(DatabaseContext databaseContext)
    {
        DbSet = databaseContext.Set<TEntity>();
    }

    public async Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken)
    {
        var added = await DbSet.AddAsync(entity, cancellationToken);

        return added.Entity;
    }

    public virtual async Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await DbSet.Where(expression).AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FirstOrDefaultAsync(expression, cancellationToken);

        return entity;
    }
}