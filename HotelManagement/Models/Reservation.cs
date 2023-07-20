﻿namespace HotelManagement.Models;

public class Reservation
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal Price { get; set; }
    public double Tax { get; set; }
    public double Discount { get; set; }
    public int GuestId { get; set; }
    public Guest? Guest { get; set; }
    public int NumberOfGuests { get; set; }
}