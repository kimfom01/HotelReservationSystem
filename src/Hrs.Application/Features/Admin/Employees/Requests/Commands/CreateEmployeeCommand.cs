using Hrs.Application.Dtos.Admin.Employees;
using MediatR;

namespace Hrs.Application.Features.Admin.Employees.Requests.Commands;

public record CreateEmployeeCommand : IRequest<GetEmployeeResponse>
{
    public CreateEmployeeRequest? EmployeeRequest { get; init; }
}