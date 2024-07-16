using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AdminDataContext _adminDataContext;

    public RoleRepository(AdminDataContext adminDataContext)
    {
        _adminDataContext = adminDataContext;
    }
    
    public async Task<Role?> GetRole(Guid roleId, CancellationToken ctx)
    {
        return await _adminDataContext.Roles.FirstOrDefaultAsync(rol => rol.Id == roleId, ctx);
    }
}