using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Client.Models.DTO
{
    public class AdminRegisterViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 2)]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Other Names are required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 2)]

        public string OtherNames { get; set; }

        [Required(ErrorMessage = "Email is required")]
        
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        
        public string Role { get; set; } = "Admin";

        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
