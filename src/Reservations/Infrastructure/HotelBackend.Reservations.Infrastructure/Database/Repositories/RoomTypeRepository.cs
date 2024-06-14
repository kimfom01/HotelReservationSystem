using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class RoomTypeRepository : AdminBaseRepository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}