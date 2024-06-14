using Hrs.Application.Dtos.Payments;
using MediatR;

namespace Hrs.Application.Features.Payments.Requests.Commands;

public record AddPaymentCommand : IRequest<GetPaymentResponse>
{
    public AddPaymentRequest? PaymentRequest { get; init; }
}