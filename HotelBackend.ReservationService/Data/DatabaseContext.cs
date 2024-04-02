using HotelBackend.ReservationService.Guest;
using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Room;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.ReservationService.Data;

public class DatabaseContext : DbContext
{
    public DbSet<HotelModel> Hotels { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<ReservationModel> Reservations { get; set; }
    public DbSet<RoomModel> Rooms { get; set; }
    public DbSet<GuestProfile> GuestProfiles { get; set; }

    public DatabaseContext(
        DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HotelModel>()
            .HasData(new HotelModel
                {
                    Id = new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                    Name = "Voronezh Hotel",
                    Location = "Voronezh, Russia"
                },
                new HotelModel
                {
                    Id = new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"),
                    Name = "Moscow Hotel",
                    Location = "Moscow, Russia"
                });

        modelBuilder.Entity<RoomModel>()
            .HasData(new RoomModel
                {
                    Id = new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                    RoomNumber = "1S",
                    Availability = true,
                    HotelId = new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                    RoomTypeId = new Guid("d9c54399-f818-4e06-8983-fd997d95346c")
                },
                new RoomModel
                {
                    Id = new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"),
                    RoomNumber = "2D",
                    Availability = true,
                    HotelId = new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                    RoomTypeId = new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9")
                },
                new RoomModel
                {
                    Id = new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"),
                    RoomNumber = "3E",
                    Availability = true,
                    HotelId = new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                    RoomTypeId = new Guid("95afd44e-3478-4d98-855b-5b541dc00005")
                },
                new RoomModel
                {
                    Id = new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"),
                    RoomNumber = "1Q",
                    Availability = false,
                    HotelId = new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"),
                    RoomTypeId = new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57")
                },
                new RoomModel
                {
                    Id = new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"),
                    RoomNumber = "2B",
                    Availability = true,
                    HotelId = new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"),
                    RoomTypeId = new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf")
                });

        modelBuilder.Entity<RoomType>()
            .HasData(new RoomType
                {
                    Id = new Guid("d9c54399-f818-4e06-8983-fd997d95346c"),
                    Type = "Standard",
                    Description = "Basic room with essential amenities, ideal for budget travelers.",
                    Capacity = 1
                },
                new RoomType
                {
                    Id = new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"),
                    Type = "Deluxe",
                    Description =
                        "Larger than a standard room with additional seating area and possibly a better view.",
                    Capacity = 1
                },
                new RoomType
                {
                    Id = new Guid("95afd44e-3478-4d98-855b-5b541dc00005"),
                    Type = "Executive",
                    Description =
                        "Designed for business travelers, often including a work desk, high-speed internet, and access to an executive lounge.",
                    Capacity = 2
                }
                , new RoomType
                {
                    Id = new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"),
                    Type = "Suite",
                    Description =
                        "A luxurious, spacious room with separate living and sleeping areas, often featuring upscale amenities.",
                    Capacity = 3
                },
                new RoomType
                {
                    Id = new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"),
                    Type = "Family",
                    Description =
                        "Tailored for families, these suites offer multiple bedrooms or a larger space with sofa beds and sometimes kitchenettes.",
                    Capacity = 4
                },
                new RoomType
                {
                    Id = new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"),
                    Type = "Penthouse",
                    Description =
                        "The pinnacle of luxury, located on the top floor with panoramic views, high-end furnishings, and exclusive amenities.",
                    Capacity = 5,
                });

        modelBuilder.Entity<Price>()
            .HasData(new Price
                {
                    Id = new Guid("ec9783a1-96b0-4734-99ac-38639dda1c35"),
                    RoomTypeId = new Guid("d9c54399-f818-4e06-8983-fd997d95346c"),
                    Value = 100M
                },
                new Price
                {
                    Id = new Guid("7c8d7dc1-2b29-446f-9575-a108129528b8"),
                    RoomTypeId = new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"),
                    Value = 150M
                },
                new Price
                {
                    Id = new Guid("5d6611bb-24db-4d73-b2bc-13b515fb49df"),
                    RoomTypeId = new Guid("95afd44e-3478-4d98-855b-5b541dc00005"),
                    Value = 200M
                },
                new Price
                {
                    Id = new Guid("d11b6257-1cb0-44ab-95d7-d4e46def9c15"),
                    RoomTypeId = new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"),
                    Value = 300M
                },
                new Price
                {
                    Id = new Guid("57f6c20f-25e4-4bc4-bbfe-55f310081878"),
                    RoomTypeId = new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"),
                    Value = 350M
                },
                new Price
                {
                    Id = new Guid("41eb8604-8018-4689-a1e1-c9c0ceb45cb0"),
                    RoomTypeId = new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"),
                    Value = 500M
                }
            );

        modelBuilder.Entity<GuestProfile>()
            .HasData(new GuestProfile
            {
                Id = new Guid("91555d72-5259-433c-a597-23eeab1da9e3"),
                FirstName = "Guest",
                LastName = "Profile",
                ContactEmail = "guestprofile@mail.com"
            });

        modelBuilder.Entity<ReservationModel>()
            .HasData(new ReservationModel
            {
                Id = new Guid("37dfb45a-77e8-4aa0-9c96-50209a772c90"),
                HotelId = new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                NumberOfGuests = 5,
                RoomId = new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                GuestProfileId = new Guid("91555d72-5259-433c-a597-23eeab1da9e3")
            });
    }
}