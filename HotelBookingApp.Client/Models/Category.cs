using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Client.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

    }
}
