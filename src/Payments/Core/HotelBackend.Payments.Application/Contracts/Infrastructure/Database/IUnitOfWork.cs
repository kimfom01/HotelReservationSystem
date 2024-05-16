namespace HotelBackend.Payments.Application.Contracts.Infrastructure.Database;

public interface IUnitOfWork
{
    public IPaymentRepository Payments { get; }

    public Task SaveChanges();
}