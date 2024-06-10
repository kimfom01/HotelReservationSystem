using HotelBackend.Admin.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Employees.Requests.Commands;

public record LoginEmployeeCommand : IRequest<LoginEmployeeResponse>
{
    public LoginEmployeeRequest? LoginEmployee { get; init; }
}