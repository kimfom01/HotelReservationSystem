using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
{
    public MaintenanceRepository(Context context) : base(context)
    {
    }
}
