using Admin.Application.Contracts.Database.Repositories;

namespace Admin.Application.Contracts.Database;

public interface IAdminUnitOfWork : IDisposable
{
    public IRoomRepository Rooms { get; }
    public IHotelRepository Hotels { get; }
    public IRoomTypeRepository RoomTypes { get; }
    public IUserRepository Users { get; }
    public IUserRoleRepository UserRoles { get; }
    public IRoleRepository Roles { get; }
    public Task<int> SaveChanges(CancellationToken cancellationToken);
}