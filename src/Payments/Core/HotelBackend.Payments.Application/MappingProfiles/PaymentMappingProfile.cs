using AutoMapper;
using HotelBackend.Common.Messages;
using HotelBackend.Payments.Application.Dtos.Payments;
using HotelBackend.Payments.Domain.Entities;

namespace HotelBackend.Payments.Application.MappingProfiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, GetPaymentResponse>().ReverseMap();
        CreateMap<Payment, AddPaymentRequest>().ReverseMap();
        CreateMap<Payment, PaymentSavedMessage>().ReverseMap();
    }
}