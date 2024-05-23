using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class PriceRepository : Repository<PriceModel>, IPriceRepository
{
    public PriceRepository(ReservationDataContext reservationDataContext) : base(reservationDataContext)
    {
    }
}