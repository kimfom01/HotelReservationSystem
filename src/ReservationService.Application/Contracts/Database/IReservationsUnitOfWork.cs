using ReservationService.Application.Contracts.Database.Repositories;

namespace ReservationService.Application.Contracts.Database;

public interface IReservationsUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IGuestProfileRepository GuestProfiles { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}