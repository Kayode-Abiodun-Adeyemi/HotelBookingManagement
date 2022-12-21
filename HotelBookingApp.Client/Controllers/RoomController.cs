using HotelBookingApp.Client.Data;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBookingApp.Client.Controllers
{
    public class RoomController : Controller
    {
        public IRoom roomContext { get; set; }
        public ICategory category { get; set; }
        public RoomController(IRoom _roomContext, ICategory _category)
        {
            roomContext = _roomContext;
            category = _category;
        }
        [HttpGet]
        public IActionResult AddRoom()
        {
            ViewBag.AllCategory = category.ListCategories();
            if (TempData["Msg1"] != null)
                ViewBag.Msg = TempData["Msg1"];
            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(RoomDTO rooom)
        {
            var room = new Room()
            {
                HasSwimmingPool = rooom.HasSwimmingPool,
                CategoryId = rooom.Id,
                HasWifi = rooom.HasWifi,
                IsBreakfastInluded = rooom.IsBreakfastInluded,
                NumberofBed = rooom.NumberofBed,
                Price = rooom.Price,
                RoomName = rooom.RoomName,
                TotalNumberAvailable = rooom.TotalNumberAvailable
            };

           var result = roomContext.AddRooms(room);
            if (result == "Fail")
            {
                ViewBag.Msg = "Server Error: Rooms cannot be added at this time";
                return View();
            }


            TempData["Msg1"] = "Rooms Successfully Added";
            room = null;
            return RedirectToAction("AddRoom");
        }

        [HttpGet]
        public IActionResult ViewHistory()
        {
            //@{
            //    var Role = @HttpContextAccessor.HttpContext.Session.GetString("Role");
            //}

            int Id = int.Parse(HttpContext.Session.GetString("UserId"));

            IEnumerable<Booking> history = roomContext.History(Id);
            if (TempData["Msg"] != null)

                ViewBag.Msg = TempData["Msg"];
            if (history == null)
                history = null;
            return View(history);
        }

        [HttpGet("{Id}")]
        public IActionResult Cancel(int Id)
        {
            var history = roomContext.Cancel(Id);
          
            TempData["Msg"] = "Your Reservation is successfully cancelled"; return RedirectToAction("ViewHistory");
        }
    }
}
