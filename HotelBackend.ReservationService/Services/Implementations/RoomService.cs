using AutoMapper;
using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services.Implementations;

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

    public async Task<Room?> GetRoom(Guid id)
    {
        return await _unitOfWork.Rooms.GetEntity(id);
    }
}