using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(ReservationDataContext reservationDataContext) : base(reservationDataContext)
    {
    }
}
