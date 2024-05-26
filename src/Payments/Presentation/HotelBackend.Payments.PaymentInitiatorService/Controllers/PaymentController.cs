using FluentValidation;
using HotelBackend.Payments.Application.Dtos.Payments;
using HotelBackend.Payments.Application.Features.Payments.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Payments.PaymentInitiatorService.Controllers;

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
    public async Task<IActionResult> PayForReservation(AddPaymentDto paymentDto, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new AddPaymentRequest
            {
                PaymentDto = paymentDto
            }, cancellationToken);

            return Ok("Payment initiated");
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Errors);
        }
    }
}