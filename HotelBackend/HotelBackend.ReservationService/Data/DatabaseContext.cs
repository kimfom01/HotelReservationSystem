using HotelBackend.ReservationService.Models;
using Microsoft.EntityFrameworkCore;
using Sqids;

namespace HotelBackend.ReservationService.Data;

public class DatabaseContext : DbContext
{
    private readonly SqidsEncoder<int> _sqidsEncoder;
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Pricing> Pricings { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DatabaseContext(
        DbContextOptions<DatabaseContext> options,
        SqidsEncoder<int> sqidsEncoder) : base(options)
    {
        _sqidsEncoder = sqidsEncoder;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
            .HasData(new Hotel
                {
                    Id = 1,
                    Name = "Voronezh Hotel",
                    Location = "Voronezh, Russia"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Moscow Hotel",
                    Location = "Moscow, Russia"
                });

        modelBuilder.Entity<Room>()
            .HasData(new Room
                {
                    Id = new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                    HotelId = 1,
                    RoomNumber = 1,
                    Capacity = 3,
                    RoomType = "Basic",
                    RoomPrice = 5000,
                    Availabilty = true
                },
                new Room
                {
                    Id = new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"),
                    HotelId = 1,
                    RoomNumber = 2,
                    Capacity = 5,
                    RoomType = "King",
                    RoomPrice = 7000,
                    Availabilty = true
                },
                new Room
                {
                    Id = new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"),
                    HotelId = 1,
                    RoomNumber = 3,
                    Capacity = 4,
                    RoomType = "Quad",
                    RoomPrice = 6000,
                    Availabilty = true
                },
                new Room
                {
                    Id = new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"),
                    HotelId = 2,
                    RoomNumber = 1,
                    Capacity = 4,
                    RoomType = "Quad",
                    RoomPrice = 6000,
                    Availabilty = false
                },
                new Room
                {
                    Id = new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"),
                    HotelId = 2,
                    RoomNumber = 2,
                    Capacity = 3,
                    RoomType = "Basic",
                    RoomPrice = 5000,
                    Availabilty = true
                });

        modelBuilder.Entity<Reservation>()
            .HasData(new Reservation
            {
                Id = 1,
                HotelId = 1,
                CheckIn = new DateTime(2023, 07, 10),
                CheckOut = new DateTime(2023, 07, 18),
                NumberOfGuests = 5,
                Price = 7000,
                Tax = 100,
                Discount = 0,
                CreationDate = new DateTime(2023, 07, 08),
                GuestEmail = "test@mail.com",
                GuestName = "Test Guest",
                RoomId = new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                BookingId = _sqidsEncoder.Encode(1)
            });
    }
}