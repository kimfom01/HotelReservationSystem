using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;

namespace Hrs.Infrastructure.Database.Repositories;

public class RoomTypeRepository : AdminBaseRepository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }
}