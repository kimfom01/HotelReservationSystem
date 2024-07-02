using Hrs.Common;
using Hrs.Domain.Entities.Common;

namespace Hrs.Domain.Entities.Admin;

public class Employee : BaseEntity
{
    internal Employee(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? Role { get; private set; }
    public string Password { get; private set; }

    public static Employee CreateEmployee(string firstName, string lastName, string email, string? role,
        string passwordHash)
    {
        var employee = new Employee(
            firstName,
            lastName,
            email,
            passwordHash);

        if (role is null)
        {
            employee.Role = Roles.Admin;
        }

        return employee;
    }
}