using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

namespace HotelBackend.Reservations.Persistence.Data;

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

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _databaseContext.SaveChangesAsync(cancellationToken);
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