using PaymentService.Application.Contracts.Database.Repositories;
using PaymentService.Domain.Payment;

namespace PaymentService.Infrastructure.Database.Repositories;

public class PaymentRepository : PaymentsBaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(PaymentDataContext dataContext) : base(dataContext)
    {
    }
}