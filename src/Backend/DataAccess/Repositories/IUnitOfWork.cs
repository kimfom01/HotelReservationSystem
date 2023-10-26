namespace DataAccess.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IReservationRoomRepository ReservationRooms { get; }

    public Task<int> SaveChanges();
}
