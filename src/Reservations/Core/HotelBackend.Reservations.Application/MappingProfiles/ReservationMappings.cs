using AutoMapper;
using HotelBackend.Common.Models;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Dtos.Reservations;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class ReservationMappings : Profile
{
    public ReservationMappings()
    {
        CreateMap<Reservation, GetReservationDetailsDto>()
            .ReverseMap();
        CreateMap<Reservation, CreateReservationDto>()
            .ReverseMap();
        CreateMap<Reservation, ReservationMessage>()
            .ForMember(resMsg =>
                    resMsg.GuestContactEmail, opt =>
                    opt.MapFrom(res =>
                        res.GuestProfile!.ContactEmail)
            )
            .ForMember(resMsg =>
                resMsg.GuestFirstName, opt =>
                opt.MapFrom(res =>
                    res.GuestProfile!.FirstName))
            .ForMember(resMsg =>
                resMsg.GuestLastName, opt =>
                opt.MapFrom(res =>
                    res.GuestProfile!.LastName))
            .ForMember(resMsg =>
                resMsg.GuestFullName, opt =>
                opt.MapFrom(res =>
                    $"{res.GuestProfile!.FirstName} {res.GuestProfile!.LastName}"))
            .ForMember(resMsg =>
                resMsg.HotelName, opt =>
                opt.MapFrom(res =>
                    res.Hotel!.Name))
            .ForMember(resMsg =>
                resMsg.HotelLocation, opt =>
                opt.MapFrom(res =>
                    res.Hotel!.Location))
            .ForMember(resMsg =>
                resMsg.RoomNumber, opt =>
                opt.MapFrom(res =>
                    res.Room!.RoomNumber))
            .ForMember(resMsg =>
                resMsg.RoomAvailability, opt =>
                opt.MapFrom(res =>
                    res.Room!.Availability))
            .ForMember(resMsg =>
                resMsg.RoomTypeId, opt =>
                opt.MapFrom(res =>
                    res.Room!.RoomTypeId))
            .ForMember(resMsg =>
                resMsg.RoomType, opt =>
                opt.MapFrom(res =>
                    res.Room!.RoomType!.Type))
            .ForMember(resMsg =>
                resMsg.RoomCapacity, opt =>
                opt.MapFrom(res =>
                    res.Room!.RoomType!.Capacity))
            .ForMember(resMsg =>
                resMsg.RoomDescription, opt =>
                opt.MapFrom(res =>
                    res.Room!.RoomType!.Description))
            .ForMember(resMsg =>
                resMsg.RoomPriceValue, opt =>
                opt.MapFrom(res =>
                    res.Room!.RoomType!.Price!.Value))
            .ReverseMap();
    }
}