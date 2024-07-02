namespace Hrs.Application.Dtos.Admin.Rooms;

public record CreateManyRoomsRequest(int Count, int Start, Guid RoomTypeId, Guid HotelId);