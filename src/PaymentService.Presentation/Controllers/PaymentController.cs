using Asp.Versioning;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Dtos.Payments;
using PaymentService.Application.Features.Payments.Commands;

namespace PaymentService.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{v:apiVersion}/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [MapToApiVersion(1)]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay")]
    [MapToApiVersion(1)]
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