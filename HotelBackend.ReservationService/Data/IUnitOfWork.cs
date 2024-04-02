using HotelBackend.ReservationService.Guest;
using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Repositories;
using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Room;

namespace HotelBackend.ReservationService.Data;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPricingRepository Prices { get; }
    public IGuestProfileRepository GuestProfiles { get; }
    public Task<int> SaveChanges();
}