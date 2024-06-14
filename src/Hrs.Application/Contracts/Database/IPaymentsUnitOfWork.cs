using Hrs.Application.Contracts.Database.Repositories;

namespace Hrs.Application.Contracts.Database;

public interface IPaymentsUnitOfWork
{
    public IPaymentRepository Payments { get; }

    public Task SaveChanges();
}