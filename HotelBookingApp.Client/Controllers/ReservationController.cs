using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Client.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
