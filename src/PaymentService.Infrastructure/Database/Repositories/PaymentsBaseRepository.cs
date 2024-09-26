using System.Linq.Expressions;
using Hrs.Common.Entities;
using Hrs.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Infrastructure.Database.Repositories;

public class PaymentsBaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;

    protected PaymentsBaseRepository(PaymentDataContext dataContext)
    {
        DbSet = dataContext.Set<TEntity>();
    }

    public async Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken)
    {
        var entityEntry = await DbSet.AddAsync(entity, cancellationToken);

        return entityEntry.Entity;
    }

    public Task AddMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        return DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return await DbSet.Where(expression).AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        var entity = await DbSet.FirstOrDefaultAsync(expression, cancellationToken);

        return entity;
    }
}