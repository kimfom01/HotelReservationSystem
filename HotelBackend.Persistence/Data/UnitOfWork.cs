using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Persistence.Data.Repositories.Implementations;

namespace HotelBackend.Persistence.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;

    public UnitOfWork(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        Reservations = new ReservationRepository(_databaseContext);
        Rooms = new RoomRepository(_databaseContext);
        Hotels = new HotelRepository(_databaseContext);
        Prices = new PriceRepository(_databaseContext);
        GuestProfiles = new GuestProfileRepository(_databaseContext);
    }

    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPriceRepository Prices { get; }
    public IGuestProfileRepository GuestProfiles { get; }

    public async Task<int> SaveChanges()
    {
        return await _databaseContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _databaseContext.Dispose();
        }
    }
}