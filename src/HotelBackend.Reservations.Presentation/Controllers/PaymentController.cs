using FluentValidation;
using HotelBackend.Reservations.Application.Dtos.Payments;
using HotelBackend.Reservations.Application.Features.Payments.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Reservations.ReservationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay")]
    public async Task<IActionResult> PayForReservation(AddPaymentRequest paymentRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new AddPaymentCommand
            {
                PaymentRequest = paymentRequest
            }, cancellationToken);

            return Ok("Payment saved");
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Errors);
        }
    }
}