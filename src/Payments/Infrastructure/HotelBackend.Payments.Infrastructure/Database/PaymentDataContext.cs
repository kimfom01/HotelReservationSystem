using HotelBackend.Payments.Domain.Entities;
using HotelBackend.Payments.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Payments.Infrastructure.Database;

public class PaymentDataContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }

    public PaymentDataContext(DbContextOptions<PaymentDataContext> options) : base(options)
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
        modelBuilder.HasDefaultSchema("payments");
    }
}