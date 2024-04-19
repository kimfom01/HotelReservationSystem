using AutoMapper;
using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Features;

public class HotelService : IHotelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HotelService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Hotel>?> GetHotels()
    {
        var hotels = await _unitOfWork.Hotels.GetEntities(hot => true);

        return _mapper.Map<IEnumerable<Hotel>>(hotels);
    }
}