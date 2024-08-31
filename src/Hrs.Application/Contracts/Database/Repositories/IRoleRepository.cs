using Hrs.Domain.Entities.Common;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IRoleRepository
{
    public Task<Role?> GetRole(Guid roleId, CancellationToken token);
    public Task<Role?> GetAdminRole(CancellationToken token);
}