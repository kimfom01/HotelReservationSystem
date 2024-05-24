﻿using HotelBackend.EmailClient.Application.Dtos.Common;

namespace HotelBackend.EmailClient.Application.Dtos;

public class RoomDto : BaseDto
{
    public string RoomNumber { get; set; } = string.Empty;
    public bool Availability { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomTypeDto? RoomType { get; set; }
}