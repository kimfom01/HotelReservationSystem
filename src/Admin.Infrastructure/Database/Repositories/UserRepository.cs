using Admin.Application.Contracts.Database.Repositories;
using Admin.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Admin.Infrastructure.Database.Repositories;

public class UserRepository : AdminBaseRepository<User>, IUserRepository
{
    private readonly AdminDataContext _context;

    public UserRepository(AdminDataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync(cancellationToken) is not null;
    }

    public async Task<User?> GetUser(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(user => user.Email == email)
            .Include(user => user.UserRoles)
            .ThenInclude(userRole => userRole.Role)
            .FirstOrDefaultAsync(cancellationToken);
    }
}