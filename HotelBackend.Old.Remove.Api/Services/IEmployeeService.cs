using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IEmployeeService
{
    public Task<Employee?> GetEmployee(int id);
    public Task<IEnumerable<Employee>?> GetEmployees();
    public Task<int> DeleteEmployee(int id);
    public Task UpdateEmployee(Employee employee);
    public Task<Employee> PostEmployee(Employee employee);
}