using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>()
            .HasData(new Guest
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                MiddleName = "Sam",
                Email = "johnsmith@gmail.com",
                Phone = "+79532423745",
                DateOfBirth = new DateTime(1990, 12, 25)
            });

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

        modelBuilder.Entity<HotelAmenity>()
            .HasData(new HotelAmenity
            {
                Id = 1,
                HotelId = 1,
                Name = "Free WiFi"
            },
            new HotelAmenity
            {
                Id = 2,
                HotelId = 1,
                Name = "Free Parking"
            },
            new HotelAmenity
            {
                Id = 3,
                HotelId = 2,
                Name = "Free WiFi"
            });

        modelBuilder.Entity<Room>()
            .HasData(new Room
            {
                Id = 1,
                HotelId = 1,
                RoomNumber = 1,
                Capacity = 3,
                RoomType = "Basic",
                RoomPrice = 5000,
                Availabilty = true
            },
            new Room
            {
                Id = 2,
                HotelId = 1,
                RoomNumber = 2,
                Capacity = 5,
                RoomType = "King",
                RoomPrice = 7000,
                Availabilty = true
            },
            new Room
            {
                Id = 3,
                HotelId = 1,
                RoomNumber = 3,
                Capacity = 4,
                RoomType = "Quad",
                RoomPrice = 6000,
                Availabilty = true
            },
            new Room
            {
                Id = 4,
                HotelId = 2,
                RoomNumber = 4,
                Capacity = 4,
                RoomType = "Quad",
                RoomPrice = 6000,
                Availabilty = false
            },
            new Room
            {
                Id = 5,
                HotelId = 1,
                RoomNumber = 5,
                Capacity = 3,
                RoomType = "Basic",
                RoomPrice = 5000,
                Availabilty = true
            },
            new Room
            {
                Id = 6,
                HotelId = 2,
                RoomNumber = 6,
                Capacity = 3,
                RoomType = "Basic",
                RoomPrice = 5000,
                Availabilty = true
            },
            new Room
            {
                Id = 7,
                HotelId = 2,
                RoomNumber = 7,
                Capacity = 3,
                RoomType = "Basic",
                RoomPrice = 5000,
                Availabilty = true
            },
            new Room
            {
                Id = 8,
                HotelId = 1,
                RoomNumber = 8,
                Capacity = 5,
                RoomType = "King",
                RoomPrice = 7000,
                Availabilty = true
            },
            new Room
            {
                Id = 9,
                HotelId = 2,
                RoomNumber = 9,
                Capacity = 5,
                RoomType = "King",
                RoomPrice = 7000,
                Availabilty = true
            },
            new Room
            {
                Id = 10,
                HotelId = 2,
                RoomNumber = 10,
                Capacity = 4,
                RoomType = "Quad",
                RoomPrice = 7000,
                Availabilty = true
            });

        modelBuilder.Entity<RoomAmenity>()
            .HasData(new RoomAmenity
            {
                Id = 1,
                RoomId = 2,
                Name = "Hair dryer"
            },
            new RoomAmenity
            {
                Id = 2,
                RoomId = 5,
                Name = "Bathtub"
            },
            new RoomAmenity
            {
                Id = 3,
                RoomId = 3,
                Name = "Bathrobes"
            });

        modelBuilder.Entity<Pricing>()
            .HasData(new Pricing
            {
                Id = 1,
                RoomId = 2,
                Date = new DateTime(2023, 12, 25),
                NumberOfGuests = 5,
                Price = 7000
            });

        modelBuilder.Entity<Reservation>()
            .HasData(new Reservation
            {
                Id = 1,
                GuestId = 1,
                HotelId = 1,
                CheckIn = new DateTime(2023, 07, 10),
                CheckOut = new DateTime(2023, 07, 18),
                NumberOfGuests = 5,
                Price = 7000,
                Tax = 100,
                Discount = 0,
                Date = new DateTime(2023, 07, 08)
            });

        modelBuilder.Entity<ReservationRoom>()
            .HasData(new ReservationRoom
            {
                Id = 1, 
                ReservationId = 1,
                RoomId = 2
            });

        modelBuilder.Entity<RoomStatus>()
            .HasData(new RoomStatus
            {
                Id = 1,
                RoomId = 2,
                GuestId = 1,
                ReservationId = 1,
                CheckIn = new DateTime(2023, 07, 10),
                CheckOut = new DateTime(2023, 07, 18)
            });

        modelBuilder.Entity<Employee>()
            .HasData(new Employee
            {
                Id = 1,
                FirstName = "Keith",
                LastName = "Sadis",
                Position = "Manager",
                Email = "keith@gmail.com",
                Phone = "+79454064421",
                HotelId = 1
            });

        modelBuilder.Entity<Service>()
            .HasData(new Service
            {
                Id = 1,
                Name = "Massage",
                Price = 1500,
                HotelId = 2
            });

        modelBuilder.Entity<Maintenance>()
            .HasData(new Maintenance
            {
                Id = 1,
                RoomId = 1,
                MaintenanceType = "Painting",
                StartDate = new DateTime(2023, 06, 02),
                EndDate = new DateTime(2023, 06, 05)
            },
            new Maintenance
            {
                Id = 2,
                RoomId = 1,
                MaintenanceType = "Fumigation",
                StartDate = new DateTime(2023, 09, 07),
                EndDate = new DateTime(2023, 09, 08)
            });

        modelBuilder.Entity<Meal>()
            .HasData(new Meal
            {
                Id = 1,
                Name = "Classic Caesar Salad",
                Type = "Lunch",
                MealPrice = 300,
                HotelId = 1
            });
    }
}
