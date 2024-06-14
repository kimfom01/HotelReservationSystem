using HotelBackend.Reservations.Application.Contracts.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class RoomTypeRepository : AdminBaseRepository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}