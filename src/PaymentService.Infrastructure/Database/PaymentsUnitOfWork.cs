using PaymentService.Application.Contracts.Database;
using PaymentService.Application.Contracts.Database.Repositories;
using PaymentService.Infrastructure.Database.Repositories;

namespace PaymentService.Infrastructure.Database;

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