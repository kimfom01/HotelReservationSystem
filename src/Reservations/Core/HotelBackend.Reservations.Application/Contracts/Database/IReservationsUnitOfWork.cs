using HotelBackend.Reservations.Application.Contracts.Database.Repositories;

namespace HotelBackend.Reservations.Application.Contracts.Database;

public interface IReservationsUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IGuestProfileRepository GuestProfiles { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}