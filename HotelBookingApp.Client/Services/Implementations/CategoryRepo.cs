using HotelBookingApp.Client.Data;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingApp.Client.Services.Implementations
{
    public class CategoryRepo : ICategory
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public bool AddCategory(Category category) 
        {
            try
            {
                this.appDbContext.Categories.AddAsync(category);
                this.appDbContext.SaveChanges();
                return true;
            }
            catch (System.Exception )
            {
                return false;
                //throw ex;
            }         
            
        }

        public List<Category> ListCategories()
        {
            return this.appDbContext.Categories.ToList();
        }

        public Category SearchCategory(int Id)
        {
          Category cate =  this.appDbContext.Categories.FirstOrDefault(x => x.Id == Id);
            if (cate == null)
                return null;
            return cate;


        }

        public bool EditCategory(Category category)
        {
            try
            {
                Category model = SearchCategory(category.Id);
                if(model != null)
                {
                    model.CategoryName = category.CategoryName;
                    model.CategoryDescription = category.CategoryDescription;
                }
                this.appDbContext.Update(model);
                this.appDbContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
                //throw ex;
            }

        }

    }
}
