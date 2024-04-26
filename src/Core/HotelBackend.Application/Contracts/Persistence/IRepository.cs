using System.Linq.Expressions;

namespace HotelBackend.Application.Contracts.Persistence;

public interface IRepository<TEntity>
{
    public Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken);
    public Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    public Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
}