using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data;

public class Context : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelAmenity> HotelAmenities { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<RoomStatus> RoomStatuses { get; set; }
    public DbSet<Pricing> Pricings { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationRoom> ReservationRooms { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomAmenity> RoomAmenities { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
}
