namespace HotelBackend.Old.Remove.DataAccess.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IReservationRoomRepository ReservationRooms { get; }
    public IEmployeeRepository Employees { get; }
    public IGuestRepository Guests { get; }
    public IHotelAmenityRepository HotelAmenities { get; }
    public IHotelRepository Hotels { get; }
    public IMaintenanceRepository Maintenances { get; }
    public IMealRepository Meals { get; }
    public IPricingRepository Pricings { get; }
    public IRoomAmenityRepository RoomAmenities { get; }
    public IRoomStatusRepository RoomStatuses { get; }
    public IServiceRepository Services { get; }

    public Task<int> SaveChanges();
}
