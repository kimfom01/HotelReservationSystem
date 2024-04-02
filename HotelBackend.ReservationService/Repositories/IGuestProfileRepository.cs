using HotelBackend.ReservationService.Guest;

namespace HotelBackend.ReservationService.Repositories;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}