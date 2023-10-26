using DataAccess.Data;

namespace DataAccess.Repositories.Implementations;

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
    }

    public IReservationRepository Reservations { get; }
    public IRoomRepository Rooms { get; }
    public IReservationRoomRepository ReservationRooms { get; }
    public IEmployeeRepository Employees { get; }
    public IGuestRepository Guests { get; }
    public IHotelAmenityRepository HotelAmenities { get; }
    public IHotelRepository Hotels { get; }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}
