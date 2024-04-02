using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Guest;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}