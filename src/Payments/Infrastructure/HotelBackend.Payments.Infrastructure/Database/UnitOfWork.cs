using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;

namespace HotelBackend.Payments.Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly PaymentDataContext _dataContext;
    public IPaymentRepository Payments { get; }

    public UnitOfWork(PaymentDataContext dataContext)
    {
        _dataContext = dataContext;
        Payments = new PaymentRepository(dataContext);
    }

    public async Task SaveChanges()
    {
        await _dataContext.SaveChangesAsync();
    }
}