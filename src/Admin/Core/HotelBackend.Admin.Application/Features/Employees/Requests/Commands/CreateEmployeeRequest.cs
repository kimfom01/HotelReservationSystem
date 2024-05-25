using HotelBackend.Admin.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Employees.Requests.Commands;

public class CreateEmployeeRequest : IRequest<GetEmployeeDto>
{
    public CreateEmployeeDto? EmployeeDto { get; set; }
}