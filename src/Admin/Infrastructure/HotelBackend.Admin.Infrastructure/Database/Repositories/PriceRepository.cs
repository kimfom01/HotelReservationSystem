using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Infrastructure.Database.Repositories;

public class PriceRepository : Repository<RoomPrice>, IPriceRepository
{
    public PriceRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}