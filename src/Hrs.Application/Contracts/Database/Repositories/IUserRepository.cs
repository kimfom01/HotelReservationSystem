using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> CheckIfUserExists(string email, CancellationToken ctx);
    Task<User?> GetUser(string email, CancellationToken ctx);
}