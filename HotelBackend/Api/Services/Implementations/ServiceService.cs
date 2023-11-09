using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class ServiceService : IServiceService
{
    private readonly IUnitOfWork _unitOfWork;

    public ServiceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Service?> GetService(int id)
    {
        return await _unitOfWork.Services.GetEntity(id);
    }

    public async Task<Service?> GetService(Expression<Func<Service, bool>> expression)
    {
        return await _unitOfWork.Services.GetEntity(expression);
    }

    public async Task<IEnumerable<Service>?> GetServices()
    {
        return await _unitOfWork.Services.GetEntities(ser => true);
    }

    public async Task<IEnumerable<Service>?> GetServices(Expression<Func<Service, bool>> expression)
    {
        return await _unitOfWork.Services.GetEntities(expression);
    }

    public async Task<int> DeleteService(int id)
    {
        await _unitOfWork.Services.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdateService(Service service)
    {
        await _unitOfWork.Services.Update(service);
        await _unitOfWork.SaveChanges();
    }

    public async Task<Service> PostService(Service service)
    {
        var added = await _unitOfWork.Services.Add(service);
        await _unitOfWork.SaveChanges();
        return added;
    }
}