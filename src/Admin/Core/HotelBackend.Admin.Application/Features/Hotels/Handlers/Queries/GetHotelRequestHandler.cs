using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Hotels;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Hotels.Handlers.Queries;

public class GetHotelRequestHandler : IRequestHandler<GetHotelRequest, GetHotelDto>
{
    private readonly ILogger<GetHotelRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetHotelRequestHandler(
        ILogger<GetHotelRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetHotelDto> Handle(GetHotelRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting hotel with id={HotelId}", request.HotelId);
        var hotel = await _unitOfWork.Hotels.GetEntity(hot =>
            hot.Id == request.HotelId && hot.AdminId == request.AdminId);

        if (hotel is null)
        {
            _logger.LogWarning("Hotel with id={HotelId} does not exist", request.HotelId);
            throw new NotFoundException($"Hotel with id={request.HotelId} does not exist");
        }

        return _mapper.Map<GetHotelDto>(hotel);
    }
}