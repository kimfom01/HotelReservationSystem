using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace HotelBackend.General.Api.Services.Implementations;

public class MaintenanceService : IMaintenanceService
{
    private readonly IUnitOfWork _unitOfWork;

    public MaintenanceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteMaintenance(int id)
    {
        await _unitOfWork.Maintenances.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<Maintenance?> GetMaintenance(int id)
    {
        return await _unitOfWork.Maintenances.GetEntity(id);
    }

    public async Task<Maintenance?> GetMaintenance(Expression<Func<Maintenance, bool>> expression)
    {
        return await _unitOfWork.Maintenances.GetEntity(expression);
    }

    public async Task<IEnumerable<Maintenance>?> GetMaintenances()
    {
        return await _unitOfWork.Maintenances.GetEntities(main => true);
    }

    public async Task<IEnumerable<Maintenance>?> GetMaintenances(Expression<Func<Maintenance, bool>> expression)
    {
        return await _unitOfWork.Maintenances.GetEntities(expression);
    }

    public async Task<Maintenance> PostMaintenance(Maintenance maintenance)
    {
        var added = await _unitOfWork.Maintenances.Add(maintenance);
        await _unitOfWork.Maintenances.SaveChanges();
        return added;
    }

    public async Task UpdateMaintenance(Maintenance maintenance)
    {
        await _unitOfWork.Maintenances.Update(maintenance);
        await _unitOfWork.SaveChanges();
    }
}