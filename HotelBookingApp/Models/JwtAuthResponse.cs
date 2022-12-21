using System;

namespace HotelBookingApp.Models
{
    [Serializable]
    public class JwtAuthResponse
    {
        public string Token { get; set; }
        public string EmailAddress { get; set; }
        public int Expires_in { get; set; }
    }
}
