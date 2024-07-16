using Hrs.Domain.Entities.Common;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IRoleRepository
{
    public Task<Role?> GetRole(Guid roleId, CancellationToken ctx);
}