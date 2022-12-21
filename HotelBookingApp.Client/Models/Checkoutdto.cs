using System;

namespace HotelBookingApp.Client.Models
{
    public class Checkoutdto
    {
        public string stripeEmail { get; set; }
        public string stripeToken { get; set; }
        public int RoomId { get; set; }

        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }
    }
}
