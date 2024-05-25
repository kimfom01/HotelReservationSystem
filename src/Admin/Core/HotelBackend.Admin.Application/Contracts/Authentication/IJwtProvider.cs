using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.Contracts.Authentication;

public interface IJwtProvider
{
    string Generate(Employee employee);
}