using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
