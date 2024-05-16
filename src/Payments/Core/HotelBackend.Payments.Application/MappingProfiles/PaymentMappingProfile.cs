using AutoMapper;
using HotelBackend.Common.Models;
using HotelBackend.Payments.Application.Dtos.Payments;
using HotelBackend.Payments.Domain.Entities;

namespace HotelBackend.Payments.Application.MappingProfiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, GetPaymentDto>().ReverseMap();
        CreateMap<Payment, AddPaymentDto>().ReverseMap();
        CreateMap<Payment, PaymentStatusMessage>().ReverseMap();
    }
}