using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Payments.Infrastructure.Database;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    protected Repository(PaymentDataContext dataContext)
    {
        _dbSet = dataContext.Set<TEntity>();
    }

    public async Task<TEntity> AddItem(TEntity item)
    {
        var entityEntry = await _dbSet.AddAsync(item);

        return entityEntry.Entity;
    }
}