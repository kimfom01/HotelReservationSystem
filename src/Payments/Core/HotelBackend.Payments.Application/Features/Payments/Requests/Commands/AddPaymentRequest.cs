using HotelBackend.Payments.Application.Dtos.Payments;
using MediatR;

namespace HotelBackend.Payments.Application.Features.Payments.Requests.Commands;

public class AddPaymentRequest : IRequest<GetPaymentDto>
{
    public AddPaymentDto PaymentDto { get; set; }
}