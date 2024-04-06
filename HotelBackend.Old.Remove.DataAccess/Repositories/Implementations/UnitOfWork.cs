using HotelBackend.Old.Remove.DataAccess.Data;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;
        Reservations = new ReservationRepository(_context);
        Rooms = new RoomRepository(_context);
        ReservationRooms = new ReservationRoomRepository(_context);
        Employees = new EmployeeRepository(_context);
        Guests = new GuestRepository(_context);
        HotelAmenities = new HotelAmenityRepository(_context);
        Hotels = new HotelRepository(_context);
        Maintenances = new MaintenanceRepository(_context);
        Meals = new MealRepository(_context);
        Pricings = new PricingRepository(_context);
        RoomAmenities = new RoomAmenityRepository(_context);
        RoomStatuses = new RoomStatusRepository(_context);
        Services = new ServiceRepository(_context);
    }

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

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}