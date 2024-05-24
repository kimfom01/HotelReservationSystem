using HotelBackend.Common.Enums;
using HotelBackend.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Infrastructure.Database;

public class ReservationDataContext : DbContext
{
   
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<GuestProfile> GuestProfiles { get; set; }

    public ReservationDataContext(
        DbContextOptions<ReservationDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reservations");

        modelBuilder.Entity<GuestProfile>()
            .HasData(new GuestProfile
            {
                Id = new Guid("91555d72-5259-433c-a597-23eeab1da9e3"),
                FirstName = "Guest",
                LastName = "Profile",
                ContactEmail = "guestprofile@mail.com"
            });

        modelBuilder.Entity<Reservation>()
            .HasData(new Reservation
            {
                Id = new Guid("37dfb45a-77e8-4aa0-9c96-50209a772c90"),
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                NumberOfGuests = 5,
                RoomId = new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                GuestProfileId = new Guid("91555d72-5259-433c-a597-23eeab1da9e3"),
                PaymentStatus = PaymentStatus.PENDING,
                ReservationStatus = ReservationStatus.PENDING
            });
    }
}