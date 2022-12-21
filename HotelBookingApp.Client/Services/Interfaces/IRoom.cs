using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using System.Collections.Generic;

namespace HotelBookingApp.Client.Services.Interfaces
{
    public interface IRoom
    {
        string AddRooms(Room room);

        string Book(BookingModel model);

        string Cancel(int Id);

        IEnumerable<Booking> History(int Id);

        IEnumerable<Room> ViewRooms(int Id);


    }

}
