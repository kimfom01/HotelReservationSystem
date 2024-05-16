using HotelBackend.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Payments.Infrastructure.Database;

public class PaymentDataContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }

    public PaymentDataContext(DbContextOptions<PaymentDataContext> options) : base(options)
    {
    }
}