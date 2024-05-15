using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Application.Contracts.Persistence;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}