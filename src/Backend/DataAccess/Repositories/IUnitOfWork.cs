namespace DataAccess.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IReservationRoomRepository ReservationRooms { get; }
    public IEmployeeRepository Employees { get; }

    public Task<int> SaveChanges();
}
