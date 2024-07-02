using Hrs.Application.Dtos.Payments;
using MediatR;

namespace Hrs.Application.Features.Payments.Commands;

public record AddPaymentCommand : IRequest<GetPaymentResponse>
{
    public AddPaymentRequest? PaymentRequest { get; init; }
}