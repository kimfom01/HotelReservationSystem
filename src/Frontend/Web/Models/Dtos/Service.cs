﻿using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class Service
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("hotelId")]
    public int HotelId { get; set; }

    [JsonPropertyName("hotel")]
    public Hotel? Hotel { get; set; }
}
