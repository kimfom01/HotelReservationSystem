namespace HotelBackend.ReservationService.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IPricingRepository Pricings { get; }
    public Task<int> SaveChanges();
}
