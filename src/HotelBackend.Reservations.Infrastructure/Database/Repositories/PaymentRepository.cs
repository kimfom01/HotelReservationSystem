using HotelBackend.Payments.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Payment;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class PaymentRepository : PaymentsBaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(PaymentDataContext dataContext) : base(dataContext)
    {
    }
}