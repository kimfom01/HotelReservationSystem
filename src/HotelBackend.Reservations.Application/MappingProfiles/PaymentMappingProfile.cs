using AutoMapper;
using HotelBackend.Common.Messages;
using HotelBackend.Reservations.Application.Dtos.Payments;
using HotelBackend.Reservations.Domain.Entities.Payment;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, GetPaymentResponse>().ReverseMap();
        CreateMap<Payment, AddPaymentRequest>().ReverseMap();
        CreateMap<Payment, PaymentSavedMessage>().ReverseMap();
    }
}