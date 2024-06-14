using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;

namespace Hrs.Infrastructure.Database.Repositories;

public class HotelRepository : AdminBaseRepository<Hotel>, IHotelRepository
{
    public HotelRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}
