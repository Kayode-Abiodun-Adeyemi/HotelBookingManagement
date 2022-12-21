using AutoMapper;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Client.Controllers
{

    public class AdminDashBoardController : Controller
    {
        private readonly IRegister register;
      

        public AdminDashBoardController(IRegister register)
        {
            this.register = register;         
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {   
            return View();
        }

        [HttpPost]
        public IActionResult AdminRegister(AdminRegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }


            bool result = this.register.AdminRegistration(registerViewModel);
            if (!result)
            {
                string ErrorMsg = "Oops! Record cannot be saved. Kindly retry...";
                DisplayErrorMessage(ErrorMsg);
                return View();
            }

            registerViewModel = null;
            ViewBag.Register = "Admin Successfully Added";
            return View(registerViewModel);
        }

        private IActionResult DisplayErrorMessage(string str)
        {
            ErrorMessage Err = new ErrorMessage()
            {

                ErrorMsg = str
            };

            return View(Err);
        }

    }
}
