using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace HotelBookingApp.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegister register;
        private readonly ICheckLogin checkLogin;
        private readonly IConfiguration configuration;
       // const string SessionName = "_Role";

        public AccountController(IRegister register, ICheckLogin checkLogin, 
                              
                                  IConfiguration _configuration)
        {
            this.register = register;
            this.checkLogin = checkLogin;
            configuration = _configuration;
      
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User ReturnedUser = ValidateLoginCredential(loginViewModel);
            if (ReturnedUser != null)
            {
                //Assign Claim and Generate Token

                var token = GenerateToken(ReturnedUser);

              //  StringContent stringContent = new StringContent(JsonConvert.SerializeObject(ReturnedUser), Encoding.UTF8, "application/json");
               // await httpClient.PostAsync("https://localhost:44373/")
                
                HttpContext.Session.SetString("JwtToken", token);

                //Retrieve the claims
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string role = jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value;

                if (role == "Customer")
                {
                    HttpContext.Session.SetString("_Role", "Customer");
                    
                    return Redirect("~/CustomerDashBoard/Index");
               
                } else if(role == "Admin")
                {
                    HttpContext.Session.SetString("_Role", "Admin");
                    return Redirect("~/AdminDashBoard/Index");
                }
                else
                {

                }
                
            }
            ViewBag.Error = "Invalid UserName or Password";
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
           
            if(!ModelState.IsValid)
            {
                
                return View();
            }

           
            bool result = this.register.Registration(registerViewModel);
            if (!result)
            {
                string ErrorMsg = "Oops! Record cannot be saved. Kindly retry...";
                DisplayErrorMessage(ErrorMsg);
                return View();
            }

            
                return RedirectToAction("Login", "Account");
        }

        private IActionResult DisplayErrorMessage(string str)
        {
            ErrorMessage Err = new ErrorMessage()
            {

                ErrorMsg = str
            };

            return View(Err);
        }

        private User ValidateLoginCredential(LoginViewModel login)
        {
            User _user = checkLogin.ValidateLogin(login);
            if(_user != null)
                return _user;
            return null;
        }

        private string GenerateToken(User model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(ClaimTypes.Role, model.Role),
                 new Claim(ClaimTypes.NameIdentifier, model.EmailAddress),
                 new Claim("LastName", model.LastName),
                 new Claim("OtherNames", model.OtherNames),
                 new Claim("UserId", model.Id.ToString()),
                  new Claim("Role", model.Role)

            };
            HttpContext.Session.SetString("UserId", model.Id.ToString());
            HttpContext.Session.SetString("Role", model.Role);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                             configuration["Jwt:Audience"],
                             claims,
                             expires: DateTime.Now.AddMinutes(50),
                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

    }
}
