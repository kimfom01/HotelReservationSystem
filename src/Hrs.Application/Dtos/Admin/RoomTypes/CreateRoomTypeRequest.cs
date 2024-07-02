namespace Hrs.Application.Dtos.Admin.RoomTypes;

public record CreateRoomTypeRequest(
    string Type,
    int Capacity,
    string Description,
    decimal RoomPrice,
    Guid HotelId,
    int NumberOfRooms,
    int RoomNumberStartFrom
);