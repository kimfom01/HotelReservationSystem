using Admin.Application.Contracts.Database.Repositories;
using Admin.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Admin.Infrastructure.Database.Repositories;

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

    public IEnumerable<Role> GetAdminRoles()
    {
        return _context.Roles.AsNoTracking();
    }

    public IEnumerable<Role> GetEmployeeRoles(CancellationToken cancellationToken)
    {
        return _context.Roles
            .Where(role => role.Name == "Employee");
    }
}
