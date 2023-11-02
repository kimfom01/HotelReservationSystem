using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(Context context) : base(context)
    {
    }
}