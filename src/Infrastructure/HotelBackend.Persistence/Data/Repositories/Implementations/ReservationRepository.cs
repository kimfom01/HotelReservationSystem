using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Persistence.Data.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}