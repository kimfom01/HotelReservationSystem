namespace Hrs.Domain.Entities.Common;

public class Role : BaseEntity
{
    public string Name { get; private set; }
    public IReadOnlyCollection<UserRole> UserRoles { get; private set; }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}