using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.ReservationService.Guest;

public class GuestProfileRepository : Repository<GuestProfile>, IGuestProfileRepository
{
    public GuestProfileRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task<GuestProfile?> GetByEmail(string email)
    {
        return await DbSet.Where(gp => gp.ContactEmail == email).FirstOrDefaultAsync();
    }
}