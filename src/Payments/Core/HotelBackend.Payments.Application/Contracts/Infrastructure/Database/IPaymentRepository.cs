using HotelBackend.Payments.Domain.Entities;

namespace HotelBackend.Payments.Application.Contracts.Infrastructure.Database;

public interface IPaymentRepository : IRepository<Payment>
{
}