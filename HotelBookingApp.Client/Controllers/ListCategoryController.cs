using HotelBookingApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListCategoryController : Controller
    {
        private readonly ICategory category;
        public ListCategoryController(ICategory category)
        {
            this.category = category;
        }

        //[Authorize(Roles =  "Customer")]
        [Route("ListCategories")]
        [HttpGet]
        public IActionResult ListCategory()
        {
            try
            {
                var model = this.category.ListCategories();
                // return View(model);
                return Ok(model);
            }
            catch (System.Exception ex)
            {
                // return StatusCode((int)System.Net.HttpStatusCode.Unauthorized, "Unauthorized Accesss");
                throw ex;
            }

        }

    }
}
