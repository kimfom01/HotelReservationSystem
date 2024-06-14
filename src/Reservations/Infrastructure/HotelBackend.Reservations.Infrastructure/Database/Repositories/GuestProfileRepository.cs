using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Reservation;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class GuestProfileRepository : Repository<GuestProfile>, IGuestProfileRepository
{
    public GuestProfileRepository(ReservationDataContext reservationDataContext) : base(reservationDataContext)
    {
    }

    public async Task<GuestProfile?> GetByEmail(string email)
    {
        return await DbSet.Where(gp => gp.ContactEmail == email).FirstOrDefaultAsync();
    }
}