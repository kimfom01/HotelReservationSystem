using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Application.Contracts.Authentication;

public interface IJwtProvider
{
    string Generate(Employee employee);
}