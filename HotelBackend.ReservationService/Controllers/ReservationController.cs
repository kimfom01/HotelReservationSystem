using System.Net;
using AutoMapper;
using FluentValidation;
using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Exceptions;
using HotelBackend.Application.Features.Reservations.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.ReservationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(
        IMediator mediator,
        IMapper mapper,
        IReservationService reservationService, ILogger<ReservationController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _reservationService = reservationService;
        _logger = logger;
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(Guid id, CancellationToken cancellationToken)
    {
        var reservation = await _reservationService.GetReservation(id, cancellationToken);

        if (reservation is null)
        {
            return NotFound();
        }

        var reservationDto = _mapper.Map<CreateReservationDto>(reservation);

        return Ok(reservationDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservations(CancellationToken cancellationToken)
    {
        var reservations = await _reservationService.GetReservations(cancellationToken);

        var reservationsDtos = _mapper.Map<IEnumerable<CreateReservationDto>>(reservations);

        return Ok(reservationsDtos);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> PostReservation(CreateReservationDto createReservationDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var added = await _mediator.Send(new CreateReservationRequest
            {
                CreateReservationDto = createReservationDto
            }, cancellationToken);

            _logger.LogInformation("Successfully made reservation with id={reservationId}", added.Id);
            return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(ValidationException),
                validationException.Message);
            return BadRequest(validationException.Errors);
        }
        catch (NotFoundException notFoundException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(NotFoundException),
                notFoundException.Message);
            return BadRequest(notFoundException.Message);
        }
        catch (NotAvailableException notAvailableException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(NotAvailableException),
                notAvailableException.Message);
            return BadRequest(notAvailableException.Message);
        }
        catch (ReservationException reservationException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(ReservationException),
                reservationException.Message);
            return BadRequest(reservationException.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError("Unable to make reservation: {exceptionMessage}", exception.Message);
            return Conflict($"Unable to make reservation: {exception.Message}");
        }
    }
}