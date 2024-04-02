using HotelBackend.ReservationService.Guest;
using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Repositories;
using HotelBackend.ReservationService.Repositories.Implementations;
using HotelBackend.ReservationService.Reservation;

namespace HotelBackend.ReservationService.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;

    public UnitOfWork(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        Reservations = new ReservationRepository(_databaseContext);
        Rooms = new RoomRepository(_databaseContext);
        Hotels = new HotelRepository(_databaseContext);
        Prices = new PricingRepository(_databaseContext);
        GuestProfiles = new GuestProfileRepository(_databaseContext);
    }

    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPricingRepository Prices { get; }
    public IGuestProfileRepository GuestProfiles { get; }

    public async Task<int> SaveChanges()
    {
        return await _databaseContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _databaseContext.Dispose();
    }
}