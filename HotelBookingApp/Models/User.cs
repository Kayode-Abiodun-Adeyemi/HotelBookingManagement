using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    public class User
    {
        public int Id { get; set; }
       
        public string LastName { get; set; }
      
        public string OtherNames { get; set; }
       
        public string EmailAddress { get; set; }
        
        public string Password { get; set; }

        public string Role { get; set; }
     
        public DateTime DateCreated { get; set; } = DateTime.Now;
        

    }
}
