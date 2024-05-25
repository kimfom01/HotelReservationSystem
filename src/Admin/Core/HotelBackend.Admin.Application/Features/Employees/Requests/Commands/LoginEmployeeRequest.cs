using HotelBackend.Admin.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Employees.Requests.Commands;

public class LoginEmployeeRequest : IRequest<EmployeeLoginResponseDto>
{
    public EmployeeLoginDto? LoginDto { get; set; }
}