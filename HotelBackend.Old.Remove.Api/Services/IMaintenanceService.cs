using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IMaintenanceService
{
    public Task<Maintenance?> GetMaintenance(int id);
    public Task<Maintenance?> GetMaintenance(Expression<Func<Maintenance, bool>> expression);
    public Task<IEnumerable<Maintenance>?> GetMaintenances();
    public Task<IEnumerable<Maintenance>?> GetMaintenances(Expression<Func<Maintenance, bool>> expression);
    public Task<int> DeleteMaintenance(int id);
    public Task UpdateMaintenance(Maintenance maintenance);
    public Task<Maintenance> PostMaintenance(Maintenance maintenance);
}
