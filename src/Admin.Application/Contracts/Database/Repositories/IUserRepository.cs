using Hrs.Common.Repositories;
using Admin.Domain.Entities.Admin;

namespace Admin.Application.Contracts.Database.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken);
    Task<User?> GetUser(string email, CancellationToken cancellationToken);
}