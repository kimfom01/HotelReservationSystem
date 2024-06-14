using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Infrastructure.Database.Repositories;

namespace Hrs.Infrastructure.Database;

public class PaymentsUnitOfWork : IPaymentsUnitOfWork
{
    private readonly PaymentDataContext _dataContext;
    public IPaymentRepository Payments { get; }

    public PaymentsUnitOfWork(PaymentDataContext dataContext)
    {
        _dataContext = dataContext;
        Payments = new PaymentRepository(dataContext);
    }

    public async Task SaveChanges()
    {
        await _dataContext.SaveChangesAsync();
    }
}