using Hrs.Domain.Entities.Common;

namespace Hrs.Domain.Entities.Admin;

public class User : BaseEntity
{
    internal User(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyCollection<Role> Roles { get; private set; }
    public IReadOnlyCollection<UserRole> UserRoles { get; private set; }
    public string Password { get; private set; }

    public static User CreateUser(
        string firstName,
        string lastName,
        string email,
        string passwordHash)
    {
        var user = new User(
            firstName,
            lastName,
            email,
            passwordHash);

        return user;
    }
}