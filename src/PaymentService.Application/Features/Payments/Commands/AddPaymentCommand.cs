using MediatR;
using PaymentService.Application.Dtos.Payments;

namespace PaymentService.Application.Features.Payments.Commands;

public record AddPaymentCommand : IRequest<GetPaymentResponse>
{
    public AddPaymentRequest? PaymentRequest { get; init; }
}