using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Dtos.Payments;
using PaymentService.Application.Features.Payments.Commands;

namespace PaymentService.Presentation.Controllers;

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
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay")]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    [ProducesResponseType<int>(StatusCodes.Status400BadRequest)]
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