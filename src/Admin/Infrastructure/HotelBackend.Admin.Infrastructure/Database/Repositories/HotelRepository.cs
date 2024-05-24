using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Infrastructure.Database.Repositories;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}
