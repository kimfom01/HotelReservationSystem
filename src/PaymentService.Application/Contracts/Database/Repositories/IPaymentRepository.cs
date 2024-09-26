using Hrs.Common.Repositories;
using PaymentService.Domain.Payment;

namespace PaymentService.Application.Contracts.Database.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
}