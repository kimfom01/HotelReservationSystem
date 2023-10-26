using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteEmployee(int id)
    {
        await _unitOfWork.Employees.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<Employee?> GetEmployee(int id)
    {
        return await _unitOfWork.Employees.GetEntity(id);
    }

    public async Task<IEnumerable<Employee>?> GetEmployees()
    {
        return await _unitOfWork.Employees.GetEntities(emp => true);
    }

    public async Task<Employee> PostEmployee(Employee employee)
    {
        var added = await _unitOfWork.Employees.Add(employee);
        await _unitOfWork.SaveChanges();
        return added;
    }

    public async Task UpdateEmployee(Employee employee)
    {
        await _unitOfWork.Employees.Update(employee);
        await _unitOfWork.SaveChanges();
    }
}