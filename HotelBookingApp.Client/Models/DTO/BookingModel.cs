using System;

namespace HotelBookingApp.Client.Models.DTO
{
    public class BookingModel
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
