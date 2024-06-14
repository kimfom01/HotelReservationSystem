using HotelBackend.Reservations.Application.Dtos.Payments;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Payments.Requests.Commands;

public record AddPaymentCommand : IRequest<GetPaymentResponse>
{
    public AddPaymentRequest? PaymentRequest { get; init; }
}