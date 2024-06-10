using HotelBackend.Admin.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Employees.Requests.Queries;

public record GetEmployeeQuery : IRequest<GetEmployeeResponse>
{
    public Guid EmployeeId { get; init; }
}