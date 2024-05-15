using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

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