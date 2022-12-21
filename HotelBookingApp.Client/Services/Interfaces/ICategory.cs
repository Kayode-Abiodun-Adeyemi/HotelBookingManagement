using HotelBookingApp.Client.Models;
using System.Collections.Generic;

namespace HotelBookingApp.Client.Services.Interfaces
{
    public interface ICategory
    {
       bool  AddCategory(Category category);

        List<Category> ListCategories();

        Category SearchCategory(int Id);

    }
}
