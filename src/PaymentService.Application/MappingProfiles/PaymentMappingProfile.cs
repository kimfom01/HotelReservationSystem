using AutoMapper;
using PaymentService.Application.Contracts.MessageBroker;
using PaymentService.Application.Dtos.Payments;
using PaymentService.Domain.Payment;

namespace PaymentService.Application.MappingProfiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, GetPaymentResponse>().ReverseMap();
        CreateMap<Payment, AddPaymentRequest>().ReverseMap();
        CreateMap<Payment, PaymentSavedEvent>().ReverseMap();
    }
}