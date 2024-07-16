using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Common;
using Hrs.Infrastructure.Database.Repositories;

namespace Hrs.Infrastructure.Database;

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
    public IUserRepository Users { get; set; }
    public IUserRoleRepository UserRoles { get; }
    public IRoleRepository Roles { get; set; }


    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
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