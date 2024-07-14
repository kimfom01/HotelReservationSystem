using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class EmployeeRepository : AdminBaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }

    public async Task<bool> CheckIfEmployeeExists(string email)
    {
        return await DbSet.FirstOrDefaultAsync(emp => emp.Email == email) is not null;
    }
}