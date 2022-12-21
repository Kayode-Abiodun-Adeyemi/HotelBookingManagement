using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Client.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [Route("ErrorHandler/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int StatusCode)
        {
            switch (StatusCode)
            {
                case 403:
                    ViewBag.ErrorMessage = "You are forbidden to access the resource";
                    break;
            }
            return View("RestrictedError");
        }
    }
}
