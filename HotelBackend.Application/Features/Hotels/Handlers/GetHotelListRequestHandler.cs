using AutoMapper;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Dtos;
using HotelBackend.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Hotels.Handlers;

public class GetHotelListRequestHandler : IRequestHandler<GetHotelListRequest, List<HotelDto>>
{
    private readonly ILogger<GetHotelListRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetHotelListRequestHandler(
        ILogger<GetHotelListRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<HotelDto>> Handle(GetHotelListRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all hotels");
        var hotels = await _unitOfWork.Hotels.GetEntities(hot => true);

        if (hotels.Count == 0)
        {
            _logger.LogWarning("There are no hotels found or registered!");
            return [];
        }

        return _mapper.Map<List<HotelDto>>(hotels);
    }
}