using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Client.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
    }
}
