using Hrs.Application.Dtos.Admin.Employees;
using MediatR;

namespace Hrs.Application.Features.Admin.Employees.Requests.Commands;

public record LoginEmployeeCommand : IRequest<LoginEmployeeResponse>
{
    public LoginEmployeeRequest? LoginEmployee { get; init; }
}