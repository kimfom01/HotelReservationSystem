using HotelBackend.Reservations.Application.Dtos.Admin.Employees;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Employees.Requests.Commands;

public record CreateEmployeeCommand : IRequest<GetEmployeeResponse>
{
    public CreateEmployeeRequest? EmployeeRequest { get; init; }
}