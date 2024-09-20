using AutoMapper;
using Hrs.Application.Contracts.MessageBroker;
using Hrs.Domain.Entities.Payment;
using Hrs.Application.Dtos.Payments;

namespace Hrs.Application.MappingProfiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, GetPaymentResponse>().ReverseMap();
        CreateMap<Payment, AddPaymentRequest>().ReverseMap();
        CreateMap<Payment, PaymentSavedEvent>().ReverseMap();
    }
}