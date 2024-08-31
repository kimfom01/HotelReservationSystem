using Hrs.Domain.Entities.Admin;

namespace Hrs.Domain.Entities.Common;

public class UserRole : BaseEntity
{
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public Guid RoleId { get; private set; }
    public Role? Role { get; private set; }

    public UserRole()
    {
        
    }

    public UserRole(Guid userId, Guid roleId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        RoleId = roleId;
    }
}