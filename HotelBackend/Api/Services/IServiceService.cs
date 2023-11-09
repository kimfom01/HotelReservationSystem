using System.Linq.Expressions;
using DataAccess.Models;

namespace Api.Services;

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