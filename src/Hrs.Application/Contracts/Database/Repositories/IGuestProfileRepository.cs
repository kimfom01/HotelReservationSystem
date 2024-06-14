using Hrs.Domain.Entities.Reservation;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}