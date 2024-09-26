using Admin.Application.Contracts.Database.Repositories;
using Admin.Domain.Entities.Admin;

namespace Admin.Infrastructure.Database.Repositories;

public class HotelRepository : AdminBaseRepository<Hotel>, IHotelRepository
{
    public HotelRepository(AdminDataContext context) : base(context)
    {
    }
}
