namespace HotelBookingApp.Client.Models
{
    public class ErrorMsg
    {
        public int httpStatus { get; set; }
        public string Header { get; set; } = "Error";
        public string Message { get; set; }
    }
}
