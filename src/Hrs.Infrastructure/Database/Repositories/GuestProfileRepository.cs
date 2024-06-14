using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Reservation;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

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