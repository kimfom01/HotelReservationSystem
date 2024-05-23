using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}