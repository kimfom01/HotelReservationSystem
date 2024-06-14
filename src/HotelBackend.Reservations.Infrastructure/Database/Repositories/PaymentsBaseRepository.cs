using System.Linq.Expressions;
using HotelBackend.Payments.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class PaymentsBaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;

    protected PaymentsBaseRepository(PaymentDataContext dataContext)
    {
        DbSet = dataContext.Set<TEntity>();
    }

    public async Task<TEntity?> Add(TEntity item, CancellationToken cancellationToken)
    {
        var entityEntry = await DbSet.AddAsync(item, cancellationToken);

        return entityEntry.Entity;
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