using HotelBackend.Reservations.Domain.Entities.Reservation;

namespace HotelBackend.Reservations.Application.Contracts.Database.Repositories;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}