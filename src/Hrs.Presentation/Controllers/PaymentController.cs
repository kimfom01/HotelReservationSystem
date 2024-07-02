using FluentValidation;
using Hrs.Application.Dtos.Payments;
using Hrs.Application.Features.Payments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hrs.Presentation.Controllers;

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