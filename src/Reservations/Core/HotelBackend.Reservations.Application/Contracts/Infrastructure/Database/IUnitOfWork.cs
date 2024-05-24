namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IGuestProfileRepository GuestProfiles { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}