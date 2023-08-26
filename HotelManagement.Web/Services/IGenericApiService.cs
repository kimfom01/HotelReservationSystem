namespace HotelManagement.Web.Services;

public interface IGenericApiService<TEntity>
{
    Task<IEnumerable<TEntity>> FetchEntities();
    Task<TEntity?> AddEntity(TEntity entity);
    Task<TEntity?> FetchEntity(int id);
    Task<bool> UpdateEntity(TEntity entity, int id);
}
