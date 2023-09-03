﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.ViewModels;

public class ServiceViewModel
{
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int HotelId { get; set; }
    public string HotelName { get; set; }
    public SelectList? Hotels { get; set; }
}
