using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Infrastructure.Database.Repositories;

namespace HotelBackend.Admin.Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly AdminDataContext _adminDataContext;

    public UnitOfWork(AdminDataContext adminDataContext)
    {
        _adminDataContext = adminDataContext;
        Rooms = new RoomRepository(_adminDataContext);
        Hotels = new HotelRepository(_adminDataContext);
        Prices = new PriceRepository(_adminDataContext);
    }

    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPriceRepository Prices { get; }

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _adminDataContext.SaveChangesAsync(cancellationToken);
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
            _adminDataContext.Dispose();
        }
    }
}