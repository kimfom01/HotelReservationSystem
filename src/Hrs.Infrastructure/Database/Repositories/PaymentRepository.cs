using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Payment;

namespace Hrs.Infrastructure.Database.Repositories;

public class PaymentRepository : PaymentsBaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(PaymentDataContext dataContext) : base(dataContext)
    {
    }
}