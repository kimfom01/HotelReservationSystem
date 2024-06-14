using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class EmployeeRepository : AdminBaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}