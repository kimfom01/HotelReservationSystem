using HotelBackend.Payments.Application.Dtos.Payments;
using MediatR;

namespace HotelBackend.Payments.Application.Features.Payments.Requests.Commands;

public record AddPaymentCommand : IRequest<GetPaymentResponse>
{
    public AddPaymentRequest? PaymentRequest { get; init; }
}