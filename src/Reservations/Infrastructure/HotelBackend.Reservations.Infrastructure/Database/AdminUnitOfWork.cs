using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;
using HotelBackend.Reservations.Infrastructure.Database.Repositories;

namespace HotelBackend.Reservations.Infrastructure.Database;

public class AdminUnitOfWork : IAdminUnitOfWork
{
    private readonly AdminDataContext _adminDataContext;

    public AdminUnitOfWork(AdminDataContext adminDataContext)
    {
        _adminDataContext = adminDataContext;
        Rooms = new RoomRepository(_adminDataContext);
        Hotels = new HotelRepository(_adminDataContext);
        RoomTypes = new RoomTypeRepository(_adminDataContext);
        Employees = new EmployeeRepository(_adminDataContext);
    }
    
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IRoomTypeRepository RoomTypes { get; }
    public IEmployeeRepository Employees { get; }


    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
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