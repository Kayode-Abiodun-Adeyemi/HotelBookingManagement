using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Client.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ICategory category;
        public CategoryController(ICategory category)
        {
            this.category = category;
        }

        ////[Authorize(Roles =  "Customer")]
        //[Route("ListCategories")]
        //[HttpGet]        //public IActionResult ListCategory()
        //{
        //    try
        //    {
        //        var model = this.category.ListCategories();
        //        // return View(model);
        //        return Ok(model);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        // return StatusCode((int)System.Net.HttpStatusCode.Unauthorized, "Unauthorized Accesss");
        //        throw ex;
        //    }

        //}

        [HttpGet]
      //  [Route("AddCategory")]
        public IActionResult AddCategory()
        {
           
            return View();
        }



        // [Authorize(Roles ="Admin")]
       
        [HttpPost]
       // [Route("AddCategory1")]
        public IActionResult AddCategory(CategoryDTO _category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var Categorry = new Category()
            {
                CategoryName = _category.CategoryName,
                CategoryDescription = _category.CategoryDescription
            };

            var result = this.category.AddCategory(Categorry);
            if (result)
            {
                ViewBag.Response = "Category Successfully Added";
                _category = null;

                return View(_category);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("{id}")]
        //  [Route("AddCategory")]
        public IActionResult EditCategory(int id)
        {
            Category result = this.category.SearchCategory(id);
            if(result != null)
            return View(result);
            return View("Not Found");
        }


        [HttpPost]
        //  [Route("AddCategory")]
        public IActionResult EditCategory(CategoryDTO model)
        {
            Category result = this.category.SearchCategory(model.Id);
            if (result != null)
                return View(result);
            return View("Not Found");
        }


    }
}
