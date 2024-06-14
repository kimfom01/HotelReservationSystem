using HotelBackend.Reservations.Domain.Entities.Reservation;

namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}