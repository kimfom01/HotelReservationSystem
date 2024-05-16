using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;
using HotelBackend.Payments.Domain.Entities;

namespace HotelBackend.Payments.Infrastructure.Database;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(PaymentDataContext dataContext) : base(dataContext)
    {
    }
}