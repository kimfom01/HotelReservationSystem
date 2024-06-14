using HotelBackend.Reservations.Application.Dtos.Admin.Employees;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Employees.Requests.Commands;

public record LoginEmployeeCommand : IRequest<LoginEmployeeResponse>
{
    public LoginEmployeeRequest? LoginEmployee { get; init; }
}