using HotelBackend.Reservations.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Employees.Requests.Queries;

public record GetEmployeeQuery : IRequest<GetEmployeeResponse>
{
    public Guid EmployeeId { get; init; }
}