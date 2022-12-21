using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models.DTO
{
    public class LoginViewModel
    {

        [Required]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
