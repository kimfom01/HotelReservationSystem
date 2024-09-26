using Admin.Application.Contracts.Database.Repositories;
using Admin.Domain.Entities.Admin;

namespace Admin.Infrastructure.Database.Repositories;

public class UserRoleRepository : AdminBaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AdminDataContext context) : base(context)
    {
    }
}