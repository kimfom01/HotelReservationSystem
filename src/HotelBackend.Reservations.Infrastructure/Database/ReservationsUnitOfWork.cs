using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Contracts.Database.Repositories;
using HotelBackend.Reservations.Infrastructure.Database.Repositories;

namespace HotelBackend.Reservations.Infrastructure.Database;

public class ReservationsUnitOfWork : IReservationsUnitOfWork
{
    private readonly ReservationDataContext _reservationDataContext;

    public ReservationsUnitOfWork(ReservationDataContext reservationDataContext)
    {
        _reservationDataContext = reservationDataContext;
        Reservations = new ReservationRepository(_reservationDataContext);
        GuestProfiles = new GuestProfileRepository(_reservationDataContext);
    }

    public IReservationRepository Reservations { get; }
    public IGuestProfileRepository GuestProfiles { get; }

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _reservationDataContext.SaveChangesAsync(cancellationToken);
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
            _reservationDataContext.Dispose();
        }
    }
}