using HotelBackend.Reservations.Application.Contracts.Database.Repositories;

namespace HotelBackend.Reservations.Application.Contracts.Database;

public interface IPaymentsUnitOfWork
{
    public IPaymentRepository Payments { get; }

    public Task SaveChanges();
}