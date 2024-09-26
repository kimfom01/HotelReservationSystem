using AutoMapper;
using Hrs.Common.Messages;
using ReservationService.Application.Contracts.MessageBroker;
using ReservationService.Application.Dtos.Reservations;
using ReservationService.Domain;

namespace ReservationService.Application.MappingProfiles;

public class ReservationMappings : Profile
{
    public ReservationMappings()
    {
        CreateMap<Reservation, GetReservationDetailsResponse>()
            .ReverseMap();
        CreateMap<Reservation, CreateReservationRequest>()
            .ReverseMap();
        CreateMap<PaymentSavedEvent, UpdateReservationPaymentStatusRequest>()
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
            .ReverseMap();
    }
}