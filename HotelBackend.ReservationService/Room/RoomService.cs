using AutoMapper;
using HotelBackend.ReservationService.Data;

namespace HotelBackend.ReservationService.Room;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsPerRoomCapacity(Guid hotelId)
    {
        var rooms = await _unitOfWork.Rooms.GetAllAvailableRooms(hotelId);

        return _mapper.Map<IEnumerable<RoomDto>>(rooms) ?? [];
    }

    public async Task<RoomModel?> GetRoom(Guid id)
    {
        return await _unitOfWork.Rooms.GetEntity(id);
    }
}