using System.Linq.Expressions;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IRepository<TEntity>
{
    public Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken);
    public Task AddMany(List<TEntity> entities, CancellationToken cancellationToken);
    public Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    public Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
}