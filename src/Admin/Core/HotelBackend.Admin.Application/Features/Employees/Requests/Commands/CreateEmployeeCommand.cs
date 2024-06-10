using HotelBackend.Admin.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Employees.Requests.Commands;

public record CreateEmployeeCommand : IRequest<GetEmployeeResponse>
{
    public CreateEmployeeRequest? EmployeeRequest { get; init; }
}