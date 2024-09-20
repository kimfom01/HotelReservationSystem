using Hrs.Domain.Entities.Common;
using Hrs.Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database;

public class PaymentDataContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }

    public PaymentDataContext(DbContextOptions<PaymentDataContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entityEntries = ChangeTracker.Entries<BaseEntity>()
            .Where(entry => entry.State == EntityState.Modified)
            .Where(entry => entry.State == EntityState.Added);

        foreach (var entry in entityEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    break;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");
    }
}