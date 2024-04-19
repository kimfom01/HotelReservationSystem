namespace HotelBackend.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPriceRepository Prices { get; }
    public IGuestProfileRepository GuestProfiles { get; }
    public Task<int> SaveChanges();
}