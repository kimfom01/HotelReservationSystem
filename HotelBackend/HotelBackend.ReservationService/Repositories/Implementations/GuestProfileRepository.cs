using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.ReservationService.Repositories.Implementations;

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