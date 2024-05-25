using HotelBackend.Admin.Application.Dtos.Employees;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Employees.Requests.Queries;

public class GetEmployeeRequest : IRequest<GetEmployeeDto>
{
    public Guid EmployeeId { get; set; }
    public Guid? HotelId { get; set; }
}