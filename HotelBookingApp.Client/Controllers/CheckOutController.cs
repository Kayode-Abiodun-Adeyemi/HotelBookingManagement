using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Threading.Tasks;

namespace HotelBookingApp.Client.Controllers
{
    public class CheckOutController : Controller
    {
        [Route("pay")]

        public async Task<dynamic> Pay(Models.PaymentModel pm)
        {
            return await MakePayment.PayAsync(pm.CardNumber, pm.Month, pm.Year, pm.CVC);
        }

        public IActionResult Charge(NewBookingModelDto newBookingModelDto)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = newBookingModelDto.stripeEmail,
                Source = newBookingModelDto.stripeToken


            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Test Payment",
                Currency = "usd",
                CustomerId = customer.Id

            });

            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                
                return RedirectToAction("BookRoom", "CustomerDashBoard",
                 //new {StartDate = checkoutdto.StartDate, EndDate = checkoutdto.EndDate, RoomId = checkoutdto.RoomId, UserId = checkoutdto .UserId});
                 new NewBookingModelDto { StartDate = newBookingModelDto.StartDate, EndDate = newBookingModelDto.EndDate, RoomId = newBookingModelDto.RoomId, UserId = newBookingModelDto.UserId });
            }
            else
            {
                return View();
            }
           // return View(); 
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult Create(string StripeEmail, string StripeToken, EmailViewModel model)
        //{
        //    var Customers = new CustomerService();
        //    var Charges = new ChargeService();

        //    var customer = Customers.Create(new CustomerCreateOptions
        //    {
        //        Email = StripeEmail,
        //        Source = StripeToken
        //    });

        //    var charge = Charges.Create(new ChargeCreateOptions
        //    {
             
        //        Amount = (long)20.00,
        //        Description = "Commission Fee",
        //        Currency = "NGN",
        //        Customer = customer.Id

        //    }); 

        //    if (charge.Status == "succeeded")
        //    {
        //        string BalanceTransactionId = charge.BalanceTransactionId;
        //        EmailViewModel result = new EmailViewModel()
        //        {
        //            StartAvailabilityDate = model.StartAvailabilityDate,
        //            EndAvailabilityDate = model.EndAvailabilityDate,
        //            LandlordEmailAddress = model.LandlordEmailAddress,
        //            Landlordfullname = model.Landlordfullname,
        //            CustomerChosenInspectionDate = DateTime.Now,
        //            PropertyAddress = model.PropertyAddress,
        //            TenantEmail = model.TenantEmail

        //        };
        //        return View(result);
        //    }


        //    return View();


        //}

    }
}
