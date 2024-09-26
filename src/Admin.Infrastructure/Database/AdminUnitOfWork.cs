using Admin.Application.Contracts.Database;
using Admin.Application.Contracts.Database.Repositories;
using Admin.Infrastructure.Database.Repositories;

namespace Admin.Infrastructure.Database;

public class AdminUnitOfWork : IAdminUnitOfWork
{
    private readonly AdminDataContext _adminDataContext;

    public AdminUnitOfWork(AdminDataContext adminDataContext)
    {
        _adminDataContext = adminDataContext;
        Rooms = new RoomRepository(_adminDataContext);
        Hotels = new HotelRepository(_adminDataContext);
        RoomTypes = new RoomTypeRepository(_adminDataContext);
        UserRoles = new UserRoleRepository(_adminDataContext);
        Users = new UserRepository(_adminDataContext);
        Roles = new RoleRepository(_adminDataContext);
    }

    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IRoomTypeRepository RoomTypes { get; }
    public IUserRepository Users { get; }
    public IUserRoleRepository UserRoles { get; }
    public IRoleRepository Roles { get; }


    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _adminDataContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _adminDataContext.Dispose();
        }
    }
}