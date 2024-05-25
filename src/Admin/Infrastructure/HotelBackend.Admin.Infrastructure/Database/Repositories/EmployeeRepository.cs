using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Infrastructure.Database.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}