using HotelBackend.Payments.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Contracts.Database.Repositories;
using HotelBackend.Reservations.Infrastructure.Database.Repositories;

namespace HotelBackend.Reservations.Infrastructure.Database;

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