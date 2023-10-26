using DataAccess.Repositories;
using System.Linq.Expressions;

namespace Api.Services.Implementations;

public class DataServiceGeneric<TEntity> : IDataServiceGeneric<TEntity>
{
    private readonly IRepository<TEntity> _repository;

    public DataServiceGeneric(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<int> DeleteEntity(int id)
    {
        await _repository.Delete(id);
        int deletedCount = await _repository.SaveChanges();

        return deletedCount;
    }

    public async Task<IEnumerable<TEntity>?> GetEntities()
    {
        var entities = await _repository.GetEntities(emp => true);

        return entities;
    }

    public async Task<IEnumerable<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await _repository.GetEntities(expression);

        return entities;
    }

    public async Task<TEntity?> GetEntity(int id)
    {
        var entity = await _repository.GetEntity(id);

        return entity;
    }

    public async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _repository.GetEntity(expression);

        return entity;
    }

    public async Task<TEntity> PostEntity(TEntity entity)
    {
        var added = await _repository.Add(entity);
        await _repository.SaveChanges();

        return added;
    }

    public async Task UpdateEntity(TEntity entity)
    {
        await _repository.Update(entity);
        await _repository.SaveChanges();
    }
}
