using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Client.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string OtherNames { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        

    }
}
