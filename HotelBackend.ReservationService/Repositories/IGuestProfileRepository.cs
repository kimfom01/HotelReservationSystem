using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories;

public interface IGuestProfileRepository : IRepository<GuestProfile>
{
    Task<GuestProfile?> GetByEmail(string email);
}