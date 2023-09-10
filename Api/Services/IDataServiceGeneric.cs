using System.Linq.Expressions;

namespace Api.Services;

public interface IDataServiceGeneric<TEntity>
{
    public Task<TEntity?> GetEntity(int id);
    public Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression);
    public Task<IEnumerable<TEntity>?> GetEntities();
    public Task<int> DeleteEntity(int id);
    public Task UpdateEntity(TEntity entity);
    public Task<TEntity> PostEntity(TEntity entity);
}
