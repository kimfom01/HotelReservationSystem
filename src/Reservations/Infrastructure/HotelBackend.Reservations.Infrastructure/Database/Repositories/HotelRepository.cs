using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class HotelRepository : AdminBaseRepository<Hotel>, IHotelRepository
{
    public HotelRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}
