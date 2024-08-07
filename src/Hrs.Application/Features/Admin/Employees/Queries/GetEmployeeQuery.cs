using Hrs.Application.Dtos.Admin.Employees;
using MediatR;

namespace Hrs.Application.Features.Admin.Employees.Queries;

public record GetEmployeeQuery : IRequest<GetEmployeeResponse>
{
    public Guid EmployeeId { get; init; }
}