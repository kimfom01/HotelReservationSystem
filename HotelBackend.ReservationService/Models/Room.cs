﻿using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Reservation;

namespace HotelBackend.ReservationService.Models;

public class Room
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; }
    public bool? Availability { get; set; } = true;
    public Guid HotelId { get; set; }
    public HotelModel? Hotel { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }

    public IEnumerable<ReservationModel>? Reservations { get; set; }
}
