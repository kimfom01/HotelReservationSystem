using Hrs.Common.Repositories;
using Hrs.Domain.Entities.Payment;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
}