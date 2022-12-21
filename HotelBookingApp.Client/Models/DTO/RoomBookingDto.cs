using System;

namespace HotelBookingApp.Client.Models.DTO
{
    public class RoomBookingDto
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
