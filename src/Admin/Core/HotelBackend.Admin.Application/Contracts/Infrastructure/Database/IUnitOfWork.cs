namespace HotelBackend.Admin.Application.Contracts.Infrastructure.Database;

public interface IUnitOfWork : IDisposable
{
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPriceRepository Prices { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}