using AutoMapper;
using Hrs.Common.Messages;
using Hrs.Domain.Entities.Reservation;
using Hrs.Application.Dtos.Reservations;

namespace Hrs.Application.MappingProfiles;

public class ReservationMappings : Profile
{
    public ReservationMappings()
    {
        CreateMap<Reservation, GetReservationDetailsResponse>()
            .ReverseMap();
        CreateMap<Reservation, CreateReservationRequest>()
            .ReverseMap();
        CreateMap<PaymentSavedMessage, UpdateReservationPaymentStatusRequest>()
            .ForMember(upd =>
                upd.PaymentId, opt =>
                opt.MapFrom(pay => pay.Id))
            .ReverseMap();
        CreateMap<Reservation, ReservationDetails>()
            .ForMember(resMsg =>
                    resMsg.GuestContactEmail, opt =>
                    opt.MapFrom(res => res.GuestProfile!.ContactEmail)
            )
            .ForMember(resMsg =>
                resMsg.GuestFirstName, opt =>
                opt.MapFrom(res => res.GuestProfile!.FirstName))
            .ForMember(resMsg =>
                resMsg.GuestLastName, opt =>
                opt.MapFrom(res => res.GuestProfile!.LastName))
            .ForMember(resMsg =>
                resMsg.GuestFullName, opt =>
                opt.MapFrom(res => $"{res.GuestProfile!.FirstName} {res.GuestProfile!.LastName}"))
            // .ForMember(resMsg =>
            //     resMsg.HotelName, opt =>
            //     opt.MapFrom(res => res.Room!.Hotel!.Name))
            // .ForMember(resMsg =>
            //     resMsg.HotelLocation, opt =>
            //     opt.MapFrom(res => res.Room!.Hotel!.Location))
            // .ForMember(resMsg =>
            //     resMsg.RoomNumber, opt =>
            //     opt.MapFrom(res => res.Room!.RoomNumber))
            // .ForMember(resMsg =>
            //     resMsg.RoomAvailability, opt =>
            //     opt.MapFrom(res => res.Room!.Availability))
            // .ForMember(resMsg =>
            //     resMsg.RoomTypeId, opt =>
            //     opt.MapFrom(res => res.Room!.RoomTypeId))
            // .ForMember(resMsg =>
            //     resMsg.RoomType, opt =>
            //     opt.MapFrom(res => res.Room!.RoomType!.Type))
            // .ForMember(resMsg =>
            //     resMsg.RoomCapacity, opt =>
            //     opt.MapFrom(res => res.Room!.RoomType!.Capacity))
            // .ForMember(resMsg =>
            //     resMsg.RoomDescription, opt =>
            //     opt.MapFrom(res => res.Room!.RoomType!.Description))
            // .ForMember(resMsg =>
            //     resMsg.RoomPriceValue, opt =>
            //     opt.MapFrom(res => res.Room!.RoomType!.Price!.Value))
            .ReverseMap();
    }
}