using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;

namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;

public interface IAdminUnitOfWork : IDisposable
{
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IRoomTypeRepository RoomTypes { get; }
    public IEmployeeRepository Employees { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}