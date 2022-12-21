using HotelBookingApp.Client.Data;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HotelBookingApp.Client.Controllers
{
    //[Route("[controller]/[action]")]

    public class CustomerDashBoardController : Controller
    {
        private readonly IConfiguration configuration;
        private static  HttpClient httpClient;
        private IRoom room;
        private AppDbContext appDbContext;
        private IReservation reservation;
        //   private readonly IHttpClientFactory httpFactory;
        public CustomerDashBoardController(IConfiguration _configuration, AppDbContext _appDbContext,
                                           IRoom _room, IReservation _reservation)
        {
            configuration = _configuration;
            httpClient = new HttpClient();
            reservation = _reservation;
            room = _room;
            appDbContext = _appDbContext;
            // this.httpFactory = httpClientFactory;
         

        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var ListofCategories = await Categories();
                if (!ListofCategories.Any())
                    return View();
                return View(ListofCategories);

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        private string GetToken()
        {
            var AccessToken = HttpContext.Session.GetString("JwtToken");
            return AccessToken;
        }

        [HttpGet]
        public async Task<IEnumerable<Categories>> Categories()
        {
            try
            {
                var Baseurl = configuration["Url:Baseurl"];
                var Token = GetToken();
                var URL = Baseurl + "api/ListCategory/ListCategories";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var Jsonres = await httpClient.GetStringAsync(URL);      
                
               var result = JsonConvert.DeserializeObject<List<Categories>>(Jsonres).ToList();
                return result;

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
           

        }

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public IActionResult ViewRooms(int Id)
        {
            var totalrooms = room.ViewRooms(Id);
            if (totalrooms != null)
                return View(totalrooms);
            return View();
        }

  [Route("[controller]/[action]/{id}/{price}")]      
  [HttpGet]
   public IActionResult BookRoom(int id, float price)
   {
            string UserId = HttpContext.Session.GetString("UserId");
            ViewBag.Userid = UserId;
            ViewBag.Roomid = id.ToString();
            ViewBag.Price = price;
            return View();
   }


        [HttpGet]
        public IActionResult ShowModal(decimal Price)
        {
            var result =appDbContext.Rooms.FirstOrDefault(x => x.Price < Price);

            ViewBag.Result = result;
            return View();
        }

        //[Route("[controller]/[action]/{id}/{price}")]
        [HttpGet]
        public IActionResult BookRoom(NewBookingModelDto newBookingModelDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var model1 = new AvailabilityDTO()
            {
                EndDate = newBookingModelDto.EndDate,
                StartDate = newBookingModelDto.StartDate,
                 Id = newBookingModelDto.RoomId
            };

            bool resp = reservation.CheckAvailability(model1);

            if(!resp)
            {
                ViewBag.Msg = "Room not available for that period";
                return View();
            }
                

            var model = new RoomBookingDto()
            {
                StartDate = newBookingModelDto.StartDate,
                EndDate = newBookingModelDto.EndDate,
                RoomId = newBookingModelDto.RoomId,
                UserId = newBookingModelDto.UserId
            };

            bool result = reservation.BookRoom(model);
            if (result)
                ViewBag.Msg = "Room successfully reserved";

           bool res = reservation.ReduceNumberofRoomsAfterBooking(model1);

            return View();
        }



    }
}
