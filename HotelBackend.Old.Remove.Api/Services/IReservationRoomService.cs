﻿using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IReservationRoomService
{
    Task<ReservationRoom?> AddReservationRoom(ReservationRoom reservationRoom);
    Task<ReservationRoom?> GetReservationRoom(int id);
    Task<ReservationRoom?> GetReservationRoom(Expression<Func<ReservationRoom, bool>> expression);
    Task<IEnumerable<ReservationRoom>?> GetReservationRooms();
    Task<IEnumerable<ReservationRoom>?> GetReservationRooms(Expression<Func<ReservationRoom, bool>> expression);
    Task<int> DeleteReservationRoom(int id);
    Task UpdateReservationRoom(ReservationRoom reservationRoom);
}