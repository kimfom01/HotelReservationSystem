namespace HotelBackend.Payments.Application.Contracts.Infrastructure.Database;

public interface IRepository<TEntity>
{
    public Task<TEntity> AddItem(TEntity item);
}