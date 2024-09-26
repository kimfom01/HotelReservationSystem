using PaymentService.Application.Contracts.Database.Repositories;

namespace PaymentService.Application.Contracts.Database;

public interface IPaymentsUnitOfWork
{
    public IPaymentRepository Payments { get; }

    public Task SaveChanges();
}