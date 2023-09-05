using DataAccess.Repositories;

namespace HotelManagement.Services;

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

    public async Task<TEntity?> GetEntity(int id)
    {
        var entity = await _repository.GetEntity(id);

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
