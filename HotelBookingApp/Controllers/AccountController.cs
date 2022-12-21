using HotelBookingApp.Models;
using HotelBookingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There is error in the Model");
                return View();
            }

            return View(registerViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("Login")]
        //public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        //{
        //    //Confirm if userLogin is not null
        //    if (loginViewModel == null || string.IsNullOrEmpty(loginViewModel.Email)
        //        || string.IsNullOrEmpty(LoginViewModel.))
        //    {
        //        return Content("All fields are required");
        //    }
        //    //Authenticate the User
        //    UserDto User = Authenticate(userLogin);


        //    if (User == null)
        //        return NotFound("User Not Found");

        //    //Generate the JWT Token
        //    var token = Generate(User);
        //    return Ok(token);


        //}

        private JwtAuthResponse Authenticate(UserLogin _userLogin)
        {



        }
        private string Generate(UserDto userDto)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,userDto.EmailAddress),
                new Claim(ClaimTypes.Role,userDto.Role),
                new Claim(ClaimTypes.Name,userDto.LastName),
                new Claim(ClaimTypes.GivenName,userDto.OtherNames)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: Credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
