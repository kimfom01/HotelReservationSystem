using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetRole(Guid roleId, CancellationToken token);
    IEnumerable<Role> GetAdminRoles();
    IEnumerable<Role> GetEmployeeRoles(CancellationToken cancellationToken);
}