using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(ReservationDataContext reservationDataContext) : base(reservationDataContext)
    {
    }

    public async Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken)
    {
        var reservation = await DbSet.Where(res => res.Id == reservationId)
            .AsNoTracking()
            .Include(res => res.GuestProfile)
            // .Include(res => res.Room)
            // .ThenInclude(room => room!.Hotel)
            .FirstOrDefaultAsync(res => res.Id == reservationId, cancellationToken);

        // TODO: need to figure out how to get all the reservation details and what it should consist of.
        // TODO: also configure the appropriate mapping
        
        return reservation;
    }
}