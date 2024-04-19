using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Persistence.Data.Repositories.Implementations;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
