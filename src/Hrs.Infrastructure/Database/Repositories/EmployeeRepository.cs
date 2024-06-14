using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;

namespace Hrs.Infrastructure.Database.Repositories;

public class EmployeeRepository : AdminBaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}