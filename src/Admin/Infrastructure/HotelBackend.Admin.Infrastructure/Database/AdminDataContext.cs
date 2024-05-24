using HotelBackend.Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Admin.Infrastructure.Database;

public class AdminDataContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }

    public AdminDataContext(
        DbContextOptions<AdminDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("admin");

        var hotelVoronezh = new Guid("772e0735-5e83-4894-aa59-d5dc56105404");
        var hotelMoscow = new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460");

        var standardRoomTypeVoronezh = new Guid("d9c54399-f818-4e06-8983-fd997d95346c");
        var deluxeRoomTypeVoronezh = new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9");
        var executiveRoomTypeVoronezh = new Guid("95afd44e-3478-4d98-855b-5b541dc00005");
        var suiteRoomTypeVoronezh = new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616");
        var familyRoomTypeVoronezh = new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57");
        var penthouseRoomTypeVoronezh = new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf");

        var standardRoomPrice = 100M;
        var deluxeRoomPrice = 150M;
        var executiveRoomPrice = 200M;
        var suiteRoomPrice = 300M;
        var familyRoomPrice = 350M;
        var penthouseRoomPrice = 500M;

        modelBuilder.Entity<Hotel>()
            .HasData(new Hotel
                {
                    Id = hotelVoronezh,
                    Name = "Voronezh Hotel",
                    Location = "Voronezh, Russia"
                },
                new Hotel
                {
                    Id = hotelMoscow,
                    Name = "Moscow Hotel",
                    Location = "Moscow, Russia"
                });

        modelBuilder.Entity<Room>()
            .HasData(new Room
                {
                    Id = new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                    RoomNumber = "1S",
                    Availability = true,
                    HotelId = hotelVoronezh,
                    RoomTypeId = standardRoomTypeVoronezh
                },
                new Room
                {
                    Id = new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"),
                    RoomNumber = "2D",
                    Availability = true,
                    HotelId = hotelVoronezh,
                    RoomTypeId = deluxeRoomTypeVoronezh
                },
                new Room
                {
                    Id = new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"),
                    RoomNumber = "3E",
                    Availability = true,
                    HotelId = hotelVoronezh,
                    RoomTypeId = executiveRoomTypeVoronezh
                },
                new Room
                {
                    Id = new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"),
                    RoomNumber = "1F",
                    Availability = false,
                    HotelId = hotelVoronezh,
                    RoomTypeId = familyRoomTypeVoronezh
                },
                new Room
                {
                    Id = new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"),
                    RoomNumber = "2P",
                    Availability = true,
                    HotelId = hotelVoronezh,
                    RoomTypeId = penthouseRoomTypeVoronezh
                });

        modelBuilder.Entity<RoomType>()
            .HasData(new RoomType
                {
                    Id = standardRoomTypeVoronezh,
                    Type = "Standard",
                    Description = "Basic room with essential amenities, ideal for budget travelers.",
                    Capacity = 1,
                    HotelId = hotelVoronezh,
                    RoomPrice = standardRoomPrice
                },
                new RoomType
                {
                    Id = deluxeRoomTypeVoronezh,
                    Type = "Deluxe",
                    Description =
                        "Larger than a standard room with additional seating area and possibly a better view.",
                    Capacity = 1,
                    HotelId = hotelVoronezh,
                    RoomPrice = deluxeRoomPrice
                },
                new RoomType
                {
                    Id = executiveRoomTypeVoronezh,
                    Type = "Executive",
                    Description =
                        "Designed for business travelers, often including a work desk, high-speed internet, and access to an executive lounge.",
                    Capacity = 2,
                    HotelId = hotelVoronezh,
                    RoomPrice = executiveRoomPrice
                }
                , new RoomType
                {
                    Id = suiteRoomTypeVoronezh,
                    Type = "Suite",
                    Description =
                        "A luxurious, spacious room with separate living and sleeping areas, often featuring upscale amenities.",
                    Capacity = 3,
                    HotelId = hotelVoronezh,
                    RoomPrice = suiteRoomPrice
                },
                new RoomType
                {
                    Id = familyRoomTypeVoronezh,
                    Type = "Family",
                    Description =
                        "Tailored for families, these suites offer multiple bedrooms or a larger space with sofa beds and sometimes kitchenettes.",
                    Capacity = 4,
                    HotelId = hotelVoronezh,
                    RoomPrice = familyRoomPrice
                },
                new RoomType
                {
                    Id = penthouseRoomTypeVoronezh,
                    Type = "Penthouse",
                    Description =
                        "The pinnacle of luxury, located on the top floor with panoramic views, high-end furnishings, and exclusive amenities.",
                    Capacity = 5,
                    HotelId = hotelVoronezh,
                    RoomPrice = penthouseRoomPrice
                });

        var standardRoomTypeMoscow = new Guid("5e2ec684-008c-41c0-a2a7-c80e70f6fe41");
        var deluxeRoomTypeMoscow = new Guid("4cd15491-e564-44b5-b2d4-645864be718f");
        var executiveRoomTypeMoscow = new Guid("408198cb-8118-4e42-990b-558046ab4785");
        var suiteRoomTypeMoscow = new Guid("323dafa4-5aaa-48df-aff2-bbb084660335");
        var familyRoomTypeMoscow = new Guid("e401adcc-7f91-4375-8574-6f5c035861df");
        var penthouseRoomTypeMoscow = new Guid("98355d46-005c-4aa5-948b-cb43a52821a8");

        modelBuilder.Entity<Room>()
            .HasData(new Room
                {
                    Id = new Guid("491a8c7e-3211-476e-a3af-e8113582cb32"),
                    RoomNumber = "1S",
                    Availability = true,
                    HotelId = hotelMoscow,
                    RoomTypeId = standardRoomTypeMoscow
                },
                new Room
                {
                    Id = new Guid("efe8c495-c2d7-4a1e-ad07-d7ee546215eb"),
                    RoomNumber = "2D",
                    Availability = true,
                    HotelId = hotelMoscow,
                    RoomTypeId = deluxeRoomTypeMoscow
                },
                new Room
                {
                    Id = new Guid("711027f4-d0f1-4005-8acc-8cbb35228d7e"),
                    RoomNumber = "3E",
                    Availability = true,
                    HotelId = hotelMoscow,
                    RoomTypeId = executiveRoomTypeMoscow
                },
                new Room
                {
                    Id = new Guid("59a47955-4957-4140-8a19-92868033a0d7"),
                    RoomNumber = "1F",
                    Availability = false,
                    HotelId = hotelMoscow,
                    RoomTypeId = familyRoomTypeMoscow
                },
                new Room
                {
                    Id = new Guid("6e0e6e38-6937-44e6-93b3-b68fdf8864e5"),
                    RoomNumber = "2P",
                    Availability = true,
                    HotelId = hotelMoscow,
                    RoomTypeId = penthouseRoomTypeMoscow
                });

        modelBuilder.Entity<RoomType>()
            .HasData(new RoomType
                {
                    Id = standardRoomTypeMoscow,
                    Type = "Standard",
                    Description = "Basic room with essential amenities, ideal for budget travelers.",
                    Capacity = 1,
                    HotelId = hotelMoscow,
                    RoomPrice = standardRoomPrice,
                },
                new RoomType
                {
                    Id = deluxeRoomTypeMoscow,
                    Type = "Deluxe",
                    Description =
                        "Larger than a standard room with additional seating area and possibly a better view.",
                    Capacity = 1,
                    HotelId = hotelMoscow,
                    RoomPrice = deluxeRoomPrice
                },
                new RoomType
                {
                    Id = executiveRoomTypeMoscow,
                    Type = "Executive",
                    Description =
                        "Designed for business travelers, often including a work desk, high-speed internet, and access to an executive lounge.",
                    Capacity = 2,
                    HotelId = hotelMoscow,
                    RoomPrice = executiveRoomPrice
                }
                , new RoomType
                {
                    Id = suiteRoomTypeMoscow,
                    Type = "Suite",
                    Description =
                        "A luxurious, spacious room with separate living and sleeping areas, often featuring upscale amenities.",
                    Capacity = 3,
                    HotelId = hotelMoscow,
                    RoomPrice = suiteRoomPrice
                },
                new RoomType
                {
                    Id = familyRoomTypeMoscow,
                    Type = "Family",
                    Description =
                        "Tailored for families, these suites offer multiple bedrooms or a larger space with sofa beds and sometimes kitchenettes.",
                    Capacity = 4,
                    HotelId = hotelMoscow,
                    RoomPrice = familyRoomPrice
                },
                new RoomType
                {
                    Id = penthouseRoomTypeMoscow,
                    Type = "Penthouse",
                    Description =
                        "The pinnacle of luxury, located on the top floor with panoramic views, high-end furnishings, and exclusive amenities.",
                    Capacity = 5,
                    HotelId = hotelMoscow,
                    RoomPrice = penthouseRoomPrice
                });
    }
}