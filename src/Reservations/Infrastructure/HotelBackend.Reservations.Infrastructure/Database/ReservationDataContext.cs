using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Domain.Entities.Common;
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.LastModifiedAt = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reservations");
    }
}