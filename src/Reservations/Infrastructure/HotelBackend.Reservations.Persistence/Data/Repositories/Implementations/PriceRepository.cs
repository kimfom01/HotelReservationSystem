using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

public class PriceRepository : Repository<PriceModel>, IPriceRepository
{
    public PriceRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}