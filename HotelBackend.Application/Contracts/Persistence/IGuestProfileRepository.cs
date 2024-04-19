using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Contracts.Persistence;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}