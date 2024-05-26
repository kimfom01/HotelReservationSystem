﻿using System.ComponentModel.DataAnnotations;
using HotelBackend.Common.Enums;
using HotelBackend.Reservations.Domain.Entities.Common;

namespace HotelBackend.Reservations.Domain.Entities;

public class Reservation : BaseEntity
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    [MaxLength(200)] public Guid? PaymentId { get; set; }
    [MaxLength(500)] public string? SpecialRequests { get; set; }
    [MaxLength(500)] public string? RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public GuestProfile? GuestProfile { get; set; }
    public Guid RoomId { get; set; }
    public Guid HotelId { get; set; }
}