using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Common;

namespace Hrs.Infrastructure.Database.Repositories;

public class UserRoleRepository : AdminBaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AdminDataContext context) : base(context)
    {
    }
}