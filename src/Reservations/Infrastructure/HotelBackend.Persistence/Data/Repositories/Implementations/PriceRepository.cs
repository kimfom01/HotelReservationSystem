using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Persistence.Data.Repositories.Implementations;

public class PriceRepository : Repository<PriceModel>, IPriceRepository
{
    public PriceRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}