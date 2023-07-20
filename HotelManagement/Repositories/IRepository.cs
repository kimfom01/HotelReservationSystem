using System.Linq.Expressions;

namespace HotelManagement.Repositories;

public interface IRepository<TEntity>
{
    public Task<TEntity> Add(TEntity entity);

    public Task Delete(int id);


    public Task Update(TEntity entity);


    public Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression);


    public Task<IQueryable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression);


    public Task<int> SaveChanges();

}
