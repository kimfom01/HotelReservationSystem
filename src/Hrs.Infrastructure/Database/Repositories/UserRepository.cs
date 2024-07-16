using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AdminDataContext _adminDataContext;

    public UserRepository(AdminDataContext adminDataContext)
    {
        _adminDataContext = adminDataContext;
    }

    public async Task<bool> CheckIfUserExists(string email, CancellationToken ctx)
    {
        return await _adminDataContext.Users
            .FirstOrDefaultAsync(emp
                => emp.Email == email, ctx) is not null;
    }

    public async Task<User?> GetUser(string email, CancellationToken ctx)
    {
        return await _adminDataContext.Users
            .FirstOrDefaultAsync(usr
                => usr.Email == email, ctx);
    }
}