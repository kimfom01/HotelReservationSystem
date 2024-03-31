using System.Linq.Expressions;

namespace HotelBackend.ReservationService.Repositories;

public interface IRepository<TEntity>
{
    public Task<TEntity?> Add(TEntity entity);
    public Task Delete(Guid id);
    public Task Update(TEntity entity);
    public Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity?> GetEntity(Guid id);
    public Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression);
}