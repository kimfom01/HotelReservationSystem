using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IRoleRepository
{
    public Task<Role?> GetRole(Guid roleId, CancellationToken token);
    public IEnumerable<Role> GetAdminRoles();
}