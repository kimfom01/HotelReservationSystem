using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken)
    {
        var reservation = await DbSet.Where(res => res.Id == reservationId)
            .Include(res => res.GuestProfile)
            .Include(res => res.Hotel)
            .Include(res => res.Room)
            .FirstOrDefaultAsync(res => res.Id == reservationId, cancellationToken);

        return reservation;
    }
}