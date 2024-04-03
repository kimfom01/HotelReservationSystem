using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IServiceService
{
    public Task<Service?> GetService(int id);
    public Task<Service?> GetService(Expression<Func<Service, bool>> expression);
    public Task<IEnumerable<Service>?> GetServices();
    public Task<IEnumerable<Service>?> GetServices(Expression<Func<Service, bool>> expression);
    public Task<int> DeleteService(int id);
    public Task UpdateService(Service service);
    public Task<Service> PostService(Service service);
}