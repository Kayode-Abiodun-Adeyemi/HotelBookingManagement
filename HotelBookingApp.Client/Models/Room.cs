using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApp.Client.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        [Display(Name = "Does it has Swimming Pool ?")]
        public string HasSwimmingPool { get; set; } = "false";
        [Display(Name = "Does it has Wifi ?")]
        public string HasWifi { get; set; } = "false";

        [Display(Name = "Is Breakfast Included ?")]
        public string IsBreakfastInluded { get; set; } = "false";
        [Required]
        [Display(Name = "Number of bed")]
        public int NumberofBed { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Total Number of rooms available")]
        public int TotalNumberAvailable { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
