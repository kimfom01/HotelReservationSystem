using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class UserRepository : AdminBaseRepository<User>, IUserRepository
{
    private readonly AdminDataContext _context;

    public UserRepository(AdminDataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfUserExists(string email, CancellationToken ctx)
    {
        return await _context.Users
            .FirstOrDefaultAsync(emp
                => emp.Email == email, ctx) is not null;
    }

    public async Task<User?> GetUser(string email, CancellationToken ctx)
    {
        return await _context.Users
            .FirstOrDefaultAsync(usr
                => usr.Email == email, ctx);
    }
}