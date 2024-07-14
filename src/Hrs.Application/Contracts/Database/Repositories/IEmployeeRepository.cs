using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<bool> CheckIfEmployeeExists(string email);
}