using AutoMapper;
using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Features;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsPerRoomCapacity(Guid hotelId)
    {
        var rooms = await _unitOfWork.Rooms.GetAllAvailableRooms(hotelId);

        return _mapper.Map<IEnumerable<Room>>(rooms) ?? [];
    }

    public async Task<Room?> GetRoom(Guid id)
    {
        return await _unitOfWork.Rooms.GetEntity(id);
    }
}