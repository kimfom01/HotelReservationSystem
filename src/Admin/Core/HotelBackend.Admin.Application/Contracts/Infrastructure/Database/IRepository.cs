using System.Linq.Expressions;

namespace HotelBackend.Admin.Application.Contracts.Infrastructure.Database;

public interface IRepository<TEntity>
{
    public Task<TEntity?> Add(TEntity entity);
    public Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression);
    public Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression);
}