using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Infrastructure.Database.Repositories;

public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}