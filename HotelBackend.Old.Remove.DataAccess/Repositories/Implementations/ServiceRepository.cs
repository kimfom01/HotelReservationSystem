using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(Context context) : base(context)
    {
    }
}