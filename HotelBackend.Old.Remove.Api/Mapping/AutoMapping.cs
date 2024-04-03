using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;

namespace HotelBackend.Old.Remove.Api.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Guest, GuestDto>().ReverseMap();
        CreateMap<HotelAmenity, HotelAmenityDto>().ReverseMap();
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<Maintenance, MaintenanceDto>().ReverseMap();
        CreateMap<Meal, MealDto>().ReverseMap();
        CreateMap<Pricing, PricingDto>().ReverseMap();
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<ReservationRoom, ReservationRoomDto>().ReverseMap();
        CreateMap<RoomAmenity, RoomAmenityDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<RoomStatus, RoomStatusDto>().ReverseMap();
        CreateMap<Service, ServiceDto>().ReverseMap();
    }
}
