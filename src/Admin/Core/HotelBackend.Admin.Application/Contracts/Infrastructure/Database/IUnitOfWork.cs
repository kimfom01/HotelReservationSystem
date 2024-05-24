namespace HotelBackend.Admin.Application.Contracts.Infrastructure.Database;

public interface IUnitOfWork : IDisposable
{
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IRoomTypeRepository RoomTypes { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}