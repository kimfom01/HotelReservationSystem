namespace HotelManagement.Web.Services;

public interface IGenericApiService<TEntity>
{
    Task<IEnumerable<TEntity>> FetchEntities();
    Task<TEntity?> AddEntity(TEntity entity);
}
