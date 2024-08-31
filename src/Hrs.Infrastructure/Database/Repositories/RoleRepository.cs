using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AdminDataContext _context;

    public RoleRepository(AdminDataContext context)
    {
        _context = context;
    }
    
    public async Task<Role?> GetRole(Guid roleId, CancellationToken token)
    {
        return await _context.Roles.FirstOrDefaultAsync(rol => rol.Id == roleId, token);
    }

    public async Task<Role?> GetAdminRole(CancellationToken token)
    {
        const string adminRole = "Admin";
        
        return await _context.Roles.FirstOrDefaultAsync(rol => rol.Name == adminRole, token);
    }
}